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
        //20160415 LCJ Included search
        //public ActionResult Index()
        public ActionResult Index(int? page, string searchString)
        {
            int pageSize = 4;
            int pageNumber = (page ?? 1);

            var partReceiveds = db.PartReceiveds.Include(p => p.Auditor).Include(p => p.InspectionType).Include(p => p.Part).Include(p => p.WasTested).Include(p => p.Vendor).Include(p => p.WhereFound).Include(p => p.ValveTestResult);

            //20160415 LCJ Included search
            if (searchString != null)
            {
                ViewBag.searchString = searchString;
                partReceiveds = partReceiveds.Where(s => s.SerialNumber.Contains(searchString));
                return View(partReceiveds.OrderBy(n => n.SerialNumber).ToPagedList(pageNumber, pageSize));
            }
            
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
        public ActionResult Create(int? id)
        {
            if (id == null) //First time the create is invoked
            {
                ViewBag.AuditorId = new SelectList(db.Auditors, "AuditorId", "AuditorName");
                ViewBag.InspectionTypeId = new SelectList(db.InspectionTypes, "InspectionTypeId", "InspectionTypeDesc");
                //20160425 LCJ duplicated the list of PartNumbers to display them without a prefix or prefixed with a "P" -- to allow Part Numbers to be scanned using a bar code reader (they add a P to the part number)
                ViewBag.PartId = new SelectList(db.Parts.Select(item => new { PartId = item.PartId, PartNumber = item.PartNumber }).Union(db.Parts.Select(row => new { PartId = row.PartId, PartNumber = "P" + row.PartNumber })).OrderBy(line => line.PartNumber);, "PartId", "PartNumber");
                //20160317 LCJ Reverse the order because Yes needs to be the default
                ViewBag.WasTestedId = new SelectList(db.WasTesteds.OrderByDescending(item => item.WasTestedId), "WasTestedId", "WasTestedDesc");
                ViewBag.VendorId = new SelectList(db.Vendors, "VendorId", "VendorDesc");
                ViewBag.WhereFoundId = new SelectList(db.WhereFounds, "WhereFoundId", "WhereFoundDesc");
                ViewBag.PartReceivedId = new SelectList(db.ValveTestResults, "PartReceivedId", "PartReceivedId");

                //20160317 LCJ Assign current date to PartReceivedDate
                ViewBag.PartReceivedDate = DateTime.Now;

                return View();
            }
            else //Create should populate fields that were entered before
            {
                PartReceived partReceived = db.PartReceiveds.Find(id);
                if (partReceived == null)
                {
                    return HttpNotFound();
                }
                ViewBag.AuditorId = new SelectList(db.Auditors, "AuditorId", "AuditorName", partReceived.AuditorId);
                ViewBag.InspectionTypeId = new SelectList(db.InspectionTypes, "InspectionTypeId", "InspectionTypeDesc", partReceived.InspectionTypeId);
                ViewBag.PartId = new SelectList(db.Parts, "PartId", "PartNumber", partReceived.PartId);
                ViewBag.WasTestedId = new SelectList(db.WasTesteds, "WasTestedId", "WasTestedDesc", partReceived.WasTestedId);
                ViewBag.VendorId = new SelectList(db.Vendors, "VendorId", "VendorDesc", partReceived.VendorId);
                ViewBag.WhereFoundId = new SelectList(db.WhereFounds, "WhereFoundId", "WhereFoundDesc", partReceived.WhereFoundId);
                ViewBag.PartReceivedId = new SelectList(db.ValveTestResults, "PartReceivedId", "PartReceivedId", partReceived.PartReceivedId);

                //20160317 LCJ Assign current date to PartReceivedDate and save context to repopulate PartReceived
                ViewBag.PartReceivedDate = DateTime.Now;
                ViewBag.IncomingDate = partReceived.IncomingDate;
                ViewBag.DateCode = partReceived.DateCode;
                ViewBag.InspectorNum = partReceived.InspectorNum;
                ViewBag.InspectorNum2 = partReceived.InspectorNum2;
                ViewBag.IndividualPartComments = partReceived.IndividualPartComments;
                ViewBag.RedTagNum = partReceived.RedTagNum;

                return View();
            }
        }

        // POST: PartsReceived/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PartReceivedId,VendorId,PartReceivedDate,AuditorId,PartId,WhereFoundId,InspectionTypeId,IncomingDate,DateCode,InspectorNum,InspectorNum2,SerialNumber,WasTestedId,IndividualPartComments,RedTagNum")] PartReceived partReceived)
        {
            if (ModelState.IsValid)
            {
                db.PartReceiveds.Add(partReceived);

                int partCategoryId = db.Parts.Find(partReceived.PartId).PartCategoryId;
                string partCategoryDesc = (db.PartCategories.Find(partCategoryId)).PartCategoryDesc;
                //20160318 LCJ Insert default specific information and edit it
                if (partReceived.WasTestedId == (db.WasTesteds.First(r => r.WasTestedDesc == "Yes").WasTestedId))
                {
                    if (partCategoryDesc == "Valve")
                    {
                        ValveTestResult valveTestResult = new ValveTestResult
                        {
                            PartReceivedId = partReceived.PartReceivedId,
                            Step03TestResultId = (db.PassFail03.First(r => r.PassFail03Desc == "undefined")).PassFail03Id,
                            Step04TestResultId = (db.PassFail04.First(r => r.PassFail04Desc == "undefined")).PassFail04Id,
                            Step05mH = null,
                            Step06mH = null,
                            Step13TestResultId = (db.PassFail13.First(r => r.PassFail13Desc == "undefined")).PassFail13Id,
                            Step10High = null,
                            Step10Low = null,
                            Step11TestResultId = (db.PassFail11.First(r => r.PassFail11Desc == "undefined")).PassFail11Id,
                            Step08TestResultId = (db.PassFail08.First(r => r.PassFail08Desc == "undefined")).PassFail08Id,
                            Step05Ohms = null,
                            Step06Ohms = null
                        };

                        db.ValveTestResults.Add(valveTestResult);
                    }
                }

                db.SaveChanges();

                if (partReceived.WasTestedId == (db.WasTesteds.First(r => r.WasTestedDesc == "Yes").WasTestedId))
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
            ViewBag.WasTestedId = new SelectList(db.WasTesteds, "WasTestedId", "WasTestedDesc", partReceived.WasTestedId);
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
            ViewBag.WasTestedId = new SelectList(db.WasTesteds, "WasTestedId", "WasTestedDesc", partReceived.WasTestedId);
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
        public ActionResult Edit([Bind(Include = "PartReceivedId,VendorId,PartReceivedDate,AuditorId,PartId,WhereFoundId,InspectionTypeId,IncomingDate,DateCode,InspectorNum,SerialNumber,WasTestedId,IndividualPartComments,RedTagNum")] PartReceived partReceived)
        {
            if (ModelState.IsValid)
            {
                db.Entry(partReceived).State = EntityState.Modified;

                //2016-03-10 LCJ Find part category and edit specific data
                int partCategoryId = db.Parts.Find(partReceived.PartId).PartCategoryId;
                string partCategoryDesc = (db.PartCategories.Find(partCategoryId)).PartCategoryDesc;
                int partReceivedId = partReceived.PartReceivedId;

                db.SaveChanges();

                if (partReceived.WasTestedId == (db.WasTesteds.First(r => r.WasTestedDesc == "Yes").WasTestedId))
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
            ViewBag.WasTestedId = new SelectList(db.WasTesteds, "WasTestedId", "WasTestedDesc", partReceived.WasTestedId);
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
            if (partReceived.WasTestedId == (db.WasTesteds.First(r => r.WasTestedDesc == "Yes").WasTestedId))
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
