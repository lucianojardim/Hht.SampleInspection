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
    public class InspectionTypesController : Controller
    {
        private SampleInspectionEntities db = new SampleInspectionEntities();

        // GET: InspectionTypes
        public ActionResult Index()
        {
            return View(db.InspectionTypes.ToList());
        }

        // GET: InspectionTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InspectionType inspectionType = db.InspectionTypes.Find(id);
            if (inspectionType == null)
            {
                return HttpNotFound();
            }
            return View(inspectionType);
        }

        // GET: InspectionTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InspectionTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InspectionTypeId,InspectionTypeDesc")] InspectionType inspectionType)
        {
            if (ModelState.IsValid)
            {
                db.InspectionTypes.Add(inspectionType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(inspectionType);
        }

        // GET: InspectionTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InspectionType inspectionType = db.InspectionTypes.Find(id);
            if (inspectionType == null)
            {
                return HttpNotFound();
            }
            return View(inspectionType);
        }

        // POST: InspectionTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InspectionTypeId,InspectionTypeDesc")] InspectionType inspectionType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inspectionType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(inspectionType);
        }

        // GET: InspectionTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InspectionType inspectionType = db.InspectionTypes.Find(id);
            if (inspectionType == null)
            {
                return HttpNotFound();
            }
            // 2016-03-10 LCJ Test if InspectionType is still in use
            bool exists = db.InspectionTypes.Any(w => w.PartReceiveds.Any(s => s.InspectionTypeId == id));
            if (exists)
            {
                return View("HttGeneralError", new HandleErrorInfo(new Exception("Inspection Type cannot be deleted because is still in use."), "InspectionTypes", "Index"));
            }
            return View(inspectionType);
        }

        // POST: InspectionTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InspectionType inspectionType = db.InspectionTypes.Find(id);
            db.InspectionTypes.Remove(inspectionType);
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
