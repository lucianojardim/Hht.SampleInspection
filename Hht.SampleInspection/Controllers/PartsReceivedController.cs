using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hht.SampleInspection.Models;
using PagedList;

namespace Hht.SampleInspection.Controllers
{
    public class PartsReceivedController : Controller
    {
        private SampleInspectionEntities db = new SampleInspectionEntities();

        // GET: PartsReceived
        //20160318 LCJ Included paging
        //public ActionResult Index()
        public ActionResult Index(int? page)
        {
            int pageSize = 4;
            int pageNumber = (page ?? 1);

            var partReceiveds = db.PartReceiveds.Include(p => p.Auditor).Include(p => p.InspectionType).Include(p => p.Part).Include(p => p.YesNo).Include(p => p.Vendor).Include(p => p.WhereFound).Include(p => p.ValveTestResult);

            //20160318 LCJ Included paging
            //return View(partReceiveds.ToList());
            return View(partReceiveds.OrderBy(n => n.PartReceivedId).ToPagedList(pageNumber, pageSize));
        }

        // GET: PartsReceived/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartReceived partReceived = db.PartReceiveds.Find(id);
            if (partReceived == null)
            {
                return HttpNotFound();
            }
            return View(partReceived);
        }

        // GET: PartsReceived/Create
        public ActionResult Create()
        {
            ViewBag.AuditorId = new SelectList(db.Auditors, "AuditorId", "AuditorName");
            ViewBag.InspectionTypeId = new SelectList(db.InspectionTypes, "InspectionTypeId", "InspectionTypeDesc");
            ViewBag.PartId = new SelectList(db.Parts, "PartId", "PartNumber");
            //20160317 LCJ Reverse the order because Yes needs to be the default
            ViewBag.WasTested = new SelectList(db.YesNoes.OrderByDescending(item => item.YesNoId), "YesNoId", "YesNoDesc");
            ViewBag.VendorId = new SelectList(db.Vendors, "VendorId", "VendorDesc");
            ViewBag.WhereFoundId = new SelectList(db.WhereFounds, "WhereFoundId", "WhereFoundDesc");
            ViewBag.PartReceivedId = new SelectList(db.ValveTestResults, "PartReceivedId", "PartReceivedId");

            //20160317 LCJ Assign current date to PartReceivedDate
            ViewBag.PartReceivedDate = DateTime.Now;

            return View();
        }

        // POST: PartsReceived/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PartReceivedId,VendorId,PartReceivedDate,AuditorId,PartId,WhereFoundId,InspectionTypeId,IncomingDate,DateCode,InspectorNum,SerialNumber,WasTested,IndividualPartComments,RedTagNum")] PartReceived partReceived)
        {
            if (ModelState.IsValid)
            {
                db.PartReceiveds.Add(partReceived);

                int partCategoryId = db.Parts.Find(partReceived.PartId).PartCategoryId;
                string partCategoryDesc = (db.PartCategories.Find(partCategoryId)).PartCategoryDesc;
                //20160318 LCJ Insert default specific information and edit it
                if (partReceived.WasTested == (db.YesNoes.First(r => r.YesNoDesc == "Yes").YesNoId))
                {
                    if (partCategoryDesc == "Valve")
                    {
                        ValveTestResult valveTestResult = new ValveTestResult
                        {
                            PartReceivedId = partReceived.PartReceivedId,
                            Step03TestResultId = (db.PassFail03.First(r => r.PassFail03Desc == "fail")).PassFail03Id,
                            Step04TestResultId = (db.PassFail04.First(r => r.PassFail04Desc == "fail")).PassFail04Id,
                            Step05mH = 0M,
                            Step06mH = 0M,
                            Step13TestResultId = (db.PassFail13.First(r => r.PassFail13Desc == "fail")).PassFail13Id,
                            Step10High = 0M,
                            Step10Low = 0M,
                            Step11TestResultId = (db.PassFail11.First(r => r.PassFail11Desc == "fail")).PassFail11Id,
                            Step08TestResultId = (db.PassFail08.First(r => r.PassFail08Desc == "fail")).PassFail08Id
                        };

                        db.ValveTestResults.Add(valveTestResult);
                    }
                }

                db.SaveChanges();

                if (partReceived.WasTested == (db.YesNoes.First(r => r.YesNoDesc == "Yes").YesNoId))
                {
                    if (partCategoryDesc == "Valve")
                    {
                        return RedirectToAction("Edit", "ValveTestResults", new { id = partReceived.PartReceivedId });
                    }
                }

                // Default redirect if partCategofyDesc is not recognized
                return RedirectToAction("Index");
            }

            ViewBag.AuditorId = new SelectList(db.Auditors, "AuditorId", "AuditorName", partReceived.AuditorId);
            ViewBag.InspectionTypeId = new SelectList(db.InspectionTypes, "InspectionTypeId", "InspectionTypeDesc", partReceived.InspectionTypeId);
            ViewBag.PartId = new SelectList(db.Parts, "PartId", "PartNumber", partReceived.PartId);
            ViewBag.WasTested = new SelectList(db.YesNoes, "YesNoId", "YesNoDesc", partReceived.WasTested);
            ViewBag.VendorId = new SelectList(db.Vendors, "VendorId", "VendorDesc", partReceived.VendorId);
            ViewBag.WhereFoundId = new SelectList(db.WhereFounds, "WhereFoundId", "WhereFoundDesc", partReceived.WhereFoundId);
            ViewBag.PartReceivedId = new SelectList(db.ValveTestResults, "PartReceivedId", "PartReceivedId", partReceived.PartReceivedId);

            //20160317 LCJ Assign current date to PartReceivedDate
            ViewBag.PartReceivedDate = partReceived.PartReceivedDate;

            return View(partReceived);
        }

        // GET: PartsReceived/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartReceived partReceived = db.PartReceiveds.Find(id);
            if (partReceived == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuditorId = new SelectList(db.Auditors, "AuditorId", "AuditorName", partReceived.AuditorId);
            ViewBag.InspectionTypeId = new SelectList(db.InspectionTypes, "InspectionTypeId", "InspectionTypeDesc", partReceived.InspectionTypeId);
            ViewBag.PartId = new SelectList(db.Parts, "PartId", "PartNumber", partReceived.PartId);
            ViewBag.WasTested = new SelectList(db.YesNoes, "YesNoId", "YesNoDesc", partReceived.WasTested);
            ViewBag.VendorId = new SelectList(db.Vendors, "VendorId", "VendorDesc", partReceived.VendorId);
            ViewBag.WhereFoundId = new SelectList(db.WhereFounds, "WhereFoundId", "WhereFoundDesc", partReceived.WhereFoundId);
            ViewBag.PartReceivedId = new SelectList(db.ValveTestResults, "PartReceivedId", "PartReceivedId", partReceived.PartReceivedId);
            return View(partReceived);
        }

        // POST: PartsReceived/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PartReceivedId,VendorId,PartReceivedDate,AuditorId,PartId,WhereFoundId,InspectionTypeId,IncomingDate,DateCode,InspectorNum,SerialNumber,WasTested,IndividualPartComments,RedTagNum")] PartReceived partReceived)
        {
            if (ModelState.IsValid)
            {
                db.Entry(partReceived).State = EntityState.Modified;

                //2016-03-10 LCJ Find part category and edit specific data
                int partCategoryId = db.Parts.Find(partReceived.PartId).PartCategoryId;
                string partCategoryDesc = (db.PartCategories.Find(partCategoryId)).PartCategoryDesc;
                int partReceivedId = partReceived.PartReceivedId;

                db.SaveChanges();

                if (partReceived.WasTested == (db.YesNoes.First(r => r.YesNoDesc == "Yes").YesNoId))
                {
                    if (partCategoryDesc == "Valve")
                    {
                        return RedirectToAction("Edit", "ValveTestResults", new { id = partReceivedId });
                    }
                }

                return RedirectToAction("Index");
            }
            ViewBag.AuditorId = new SelectList(db.Auditors, "AuditorId", "AuditorName", partReceived.AuditorId);
            ViewBag.InspectionTypeId = new SelectList(db.InspectionTypes, "InspectionTypeId", "InspectionTypeDesc", partReceived.InspectionTypeId);
            ViewBag.PartId = new SelectList(db.Parts, "PartId", "PartNumber", partReceived.PartId);
            ViewBag.WasTested = new SelectList(db.YesNoes, "YesNoId", "YesNoDesc", partReceived.WasTested);
            ViewBag.VendorId = new SelectList(db.Vendors, "VendorId", "VendorDesc", partReceived.VendorId);
            ViewBag.WhereFoundId = new SelectList(db.WhereFounds, "WhereFoundId", "WhereFoundDesc", partReceived.WhereFoundId);
            ViewBag.PartReceivedId = new SelectList(db.ValveTestResults, "PartReceivedId", "PartReceivedId", partReceived.PartReceivedId);
            return View(partReceived);
        }

        // GET: PartsReceived/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartReceived partReceived = db.PartReceiveds.Find(id);
            if (partReceived == null)
            {
                return HttpNotFound();
            }
            return View(partReceived);
        }

        // POST: PartsReceived/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PartReceived partReceived = db.PartReceiveds.Find(id);
            db.PartReceiveds.Remove(partReceived);

            int partCategoryId = db.Parts.Find(partReceived.PartId).PartCategoryId;
            string partCategoryDesc = (db.PartCategories.Find(partCategoryId)).PartCategoryDesc;
            if (partReceived.WasTested == (db.YesNoes.First(r => r.YesNoDesc == "Yes").YesNoId))
            {
                // 2016-03-10 LCJ Remove specific data related to the part received that is being deleted
                if (partCategoryDesc == "Valve")
                {
                    ValveTestResult valveTestResult = db.ValveTestResults.Find(id);
                    db.ValveTestResults.Remove(valveTestResult);
                }
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
