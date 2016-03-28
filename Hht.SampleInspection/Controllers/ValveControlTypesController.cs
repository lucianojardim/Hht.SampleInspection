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
    public class ValveControlTypesController : Controller
    {
        private SampleInspectionEntities db = new SampleInspectionEntities();

        // GET: ValveControlTypes
        public ActionResult Index()
        {
            return View(db.ValveControlTypes.ToList());
        }

        // GET: ValveControlTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ValveControlType valveControlType = db.ValveControlTypes.Find(id);
            if (valveControlType == null)
            {
                return HttpNotFound();
            }
            return View(valveControlType);
        }

        // GET: ValveControlTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ValveControlTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ValveControlTypeId,ValveControlTypeDesc")] ValveControlType valveControlType)
        {
            if (ModelState.IsValid)
            {
                db.ValveControlTypes.Add(valveControlType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(valveControlType);
        }

        // GET: ValveControlTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ValveControlType valveControlType = db.ValveControlTypes.Find(id);
            if (valveControlType == null)
            {
                return HttpNotFound();
            }
            return View(valveControlType);
        }

        // POST: ValveControlTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ValveControlTypeId,ValveControlTypeDesc")] ValveControlType valveControlType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(valveControlType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(valveControlType);
        }

        // GET: ValveControlTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ValveControlType valveControlType = db.ValveControlTypes.Find(id);
            if (valveControlType == null)
            {
                return HttpNotFound();
            }
            // 2016-03-10 LCJ Test if ValveControl Type is still in use
            bool exists = db.ValveControlTypes.Any(w => w.Valves.Any(s => s.ValveControlTypeId == id));
            if (exists)
            {
                return View("HttGeneralError", new HandleErrorInfo(new Exception("Valve Control Type cannot be deleted because is still in use."), "ValveControlTypes", "Index"));
            }
            return View(valveControlType);
        }

        // POST: ValveControlTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ValveControlType valveControlType = db.ValveControlTypes.Find(id);
            db.ValveControlTypes.Remove(valveControlType);
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
