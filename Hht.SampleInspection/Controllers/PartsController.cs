using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hht.SampleInspection.Models;

namespace Hht.SampleInspection.Controllers
{
    public class PartsController : Controller
    {
        private SampleInspectionEntities db = new SampleInspectionEntities();

        // GET: Parts
        public ActionResult Index()
        {
            var parts = db.Parts.Include(p => p.PartCategory).Include(p => p.Valve);
            return View(parts.ToList());
        }

        // GET: Parts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Part part = db.Parts.Find(id);
            if (part == null)
            {
                return HttpNotFound();
            }
            return View(part);
        }

        // GET: Parts/Create
        public ActionResult Create()
        {
            ViewBag.PartCategoryId = new SelectList(db.PartCategories, "PartCategoryId", "PartCategoryDesc");
            ViewBag.PartId = new SelectList(db.Valves, "PartId", "PartId");
            return View();
        }

        // POST: Parts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PartId,PartNumber,PartCategoryId")] Part part)
        {
            // 2016-03-10 LCJ Test if Part is duplicated
            bool exists = db.Parts.Any(item => item.PartNumber == part.PartNumber && item.PartCategoryId == part.PartCategoryId);
            if (exists)
            {
                return View("HttpGeneralError", new HandleErrorInfo(new Exception("Part cannot be inserted because it is duplicated."), "Parts", "Index"));
            }
            if ((ModelState.IsValid) || exists)
            {
                db.Parts.Add(part);

                //2016-03-10 LCJ Insert default specific information and edit it
                string partCategoryDesc = (db.PartCategories.Find(part.PartCategoryId)).PartCategoryDesc;
                if (partCategoryDesc == "Valve")
                {
                    Valve valve = new Valve
                    {
                        PartId = part.PartId,
                        Step10LowMin = 00000000.00M,
                        Step10LowMax = 99999999.99M,
                        Step10HighMin = 00000000.00M,
                        Step10HighMax = 99999999.99M,
                        Step5mHMin = 0000.0M,
                        Step5mHMax = 9999.9M,
                        Step6mHMin = 0000.0M,
                        Step6mHMax = 9999.9M,
                        ValveControlTypeId = (db.ValveControlTypes.First(t => t.ValveControlTypeDesc == "Undefined")).ValveControlTypeId,
                        ValveFuelTypeId = (db.ValveFuelTypes.First(t => t.ValveFuelTypeDesc == "Undefined")).ValveFuelTypeId
                    };

                    db.Valves.Add(valve);
                }

                db.SaveChanges();

                if (partCategoryDesc == "Valve")
                {
                    return RedirectToAction("Edit", "Valves", new { id = part.PartId });
                }

                // Default redirect if partCategofyDesc is not recognized
                return RedirectToAction("Index");
            }

            ViewBag.PartCategoryId = new SelectList(db.PartCategories, "PartCategoryId", "PartCategoryDesc", part.PartCategoryId);
            ViewBag.PartId = new SelectList(db.Valves, "PartId", "PartId", part.PartId);
            return View(part);
        }

        // GET: Parts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Part part = db.Parts.Find(id);
            if (part == null)
            {
                return HttpNotFound();
            }
            ViewBag.PartCategoryId = new SelectList(db.PartCategories, "PartCategoryId", "PartCategoryDesc", part.PartCategoryId);
            ViewBag.PartId = new SelectList(db.Valves, "PartId", "PartId", part.PartId);
            return View(part);
        }

        // POST: Parts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PartId,PartNumber,PartCategoryId")] Part part)
        {
            // 2016-03-10 LCJ Test if Part being updated became a duplicate
            bool exists = db.Parts.Any(item => item.PartNumber == part.PartNumber && item.PartCategoryId == part.PartCategoryId && item.PartId != part.PartId);
            if (exists)
            {
                return View("HttpGeneralError", new HandleErrorInfo(new Exception("Part cannot be updated because it became duplicated."), "Parts", "Index"));
            }
            if ((ModelState.IsValid) || exists)
            {
                db.Entry(part).State = EntityState.Modified;

                //2016-03-10 LCJ Find part category and edit specific data
                string partCategoryDesc = db.PartCategories.Find(part.PartCategoryId).PartCategoryDesc;
                int partId = part.PartId;

                db.SaveChanges();

                if(partCategoryDesc == "Valve")
                {
                    return RedirectToAction("Edit", "Valves", new { id = partId });
                }

                // Default redirect if partCategofyDesc is not recognized
                return RedirectToAction("Index");
            }
            ViewBag.PartCategoryId = new SelectList(db.PartCategories, "PartCategoryId", "PartCategoryDesc", part.PartCategoryId);
            ViewBag.PartId = new SelectList(db.Valves, "PartId", "PartId", part.PartId);
            return View(part);
        }

        // GET: Parts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Part part = db.Parts.Find(id);
            if (part == null)
            {
                return HttpNotFound();
            }
            // 2016-03-10 LCJ Test if Part is still in use
            bool exists = db.Parts.Any(w => w.PartReceiveds.Any(s => s.PartId == id));
            if (exists)
            {
                return View("HttpGeneralError", new HandleErrorInfo(new Exception("Part cannot be deleted because is still in use."), "Parts", "Index"));
            }
            return View(part);
        }

        // POST: Parts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Part part = db.Parts.Find(id);
            db.Parts.Remove(part);

            string partCategoryDesc = (db.PartCategories.Find(part.PartCategoryId)).PartCategoryDesc;
            // 2016-03-10 LCJ Remove specific data related to the part that is being deleted
            if (partCategoryDesc == "Valve")
            {
                Valve valve = db.Valves.Find(id);
                db.Valves.Remove(valve);
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
