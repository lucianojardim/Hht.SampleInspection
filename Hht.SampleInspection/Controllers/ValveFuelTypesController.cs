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
    public class ValveFuelTypesController : Controller
    {
        private SampleInspectionEntities db = new SampleInspectionEntities();

        // GET: ValveFuelTypes
        public ActionResult Index()
        {
            return View(db.ValveFuelTypes.ToList());
        }

        // GET: ValveFuelTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ValveFuelType valveFuelType = db.ValveFuelTypes.Find(id);
            if (valveFuelType == null)
            {
                return HttpNotFound();
            }
            return View(valveFuelType);
        }

        // GET: ValveFuelTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ValveFuelTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ValveFuelTypeId,ValveFuelTypeDesc")] ValveFuelType valveFuelType)
        {
            if (ModelState.IsValid)
            {
                db.ValveFuelTypes.Add(valveFuelType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(valveFuelType);
        }

        // GET: ValveFuelTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ValveFuelType valveFuelType = db.ValveFuelTypes.Find(id);
            if (valveFuelType == null)
            {
                return HttpNotFound();
            }
            return View(valveFuelType);
        }

        // POST: ValveFuelTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ValveFuelTypeId,ValveFuelTypeDesc")] ValveFuelType valveFuelType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(valveFuelType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(valveFuelType);
        }

        // GET: ValveFuelTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ValveFuelType valveFuelType = db.ValveFuelTypes.Find(id);
            if (valveFuelType == null)
            {
                return HttpNotFound();
            }
            // 2016-03-10 LCJ Test if ValveFuelType is still in use
            bool exists = db.ValveFuelTypes.Any(w => w.Valves.Any(s => s.ValveFuelTypeId == id));
            if (exists)
            {
                return View("HttGeneralError", new HandleErrorInfo(new Exception("Valve Fuel Type cannot be deleted because is still in use."), "ValveFuelTypes", "Index"));
            }
            return View(valveFuelType);
        }

        // POST: ValveFuelTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ValveFuelType valveFuelType = db.ValveFuelTypes.Find(id);
            db.ValveFuelTypes.Remove(valveFuelType);
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
