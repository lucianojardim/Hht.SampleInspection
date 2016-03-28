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
    public class ValvesController : Controller
    {
        private SampleInspectionEntities db = new SampleInspectionEntities();

        // GET: Valves
        public ActionResult Index()
        {
            var valves = db.Valves.Include(v => v.Part).Include(v => v.ValveControlType).Include(v => v.ValveFuelType);
            return View(valves.ToList());
        }

        // GET: Valves/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Valve valve = db.Valves.Find(id);
            if (valve == null)
            {
                return HttpNotFound();
            }
            return View(valve);
        }

        // GET: Valves/Create
        public ActionResult Create()
        {
            ViewBag.PartId = new SelectList(db.Parts, "PartId", "PartNumber");
            ViewBag.ValveControlTypeId = new SelectList(db.ValveControlTypes, "ValveControlTypeId", "ValveControlTypeDesc");
            ViewBag.ValveFuelTypeId = new SelectList(db.ValveFuelTypes, "ValveFuelTypeId", "ValveFuelTypeDesc");
            return View();
        }

        // POST: Valves/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PartId,Step10LowMin,Step10LowMax,Step10HighMin,Step10HighMax,Step5mHMin,Step5mHMax,Step6mHMin,Step6mHMax,ValveControlTypeId,ValveFuelTypeId")] Valve valve)
        {
            if (ModelState.IsValid)
            {
                db.Valves.Add(valve);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PartId = new SelectList(db.Parts, "PartId", "PartNumber", valve.PartId);
            ViewBag.ValveControlTypeId = new SelectList(db.ValveControlTypes, "ValveControlTypeId", "ValveControlTypeDesc", valve.ValveControlTypeId);
            ViewBag.ValveFuelTypeId = new SelectList(db.ValveFuelTypes, "ValveFuelTypeId", "ValveFuelTypeDesc", valve.ValveFuelTypeId);
            return View(valve);
        }

        // GET: Valves/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Valve valve = db.Valves.Find(id);
            if (valve == null)
            {
                return HttpNotFound();
            }
            ViewBag.PartId = new SelectList(db.Parts, "PartId", "PartNumber", valve.PartId);
            ViewBag.ValveControlTypeId = new SelectList(db.ValveControlTypes, "ValveControlTypeId", "ValveControlTypeDesc", valve.ValveControlTypeId);
            ViewBag.ValveFuelTypeId = new SelectList(db.ValveFuelTypes, "ValveFuelTypeId", "ValveFuelTypeDesc", valve.ValveFuelTypeId);
            return View(valve);
        }

        // POST: Valves/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PartId,Step10LowMin,Step10LowMax,Step10HighMin,Step10HighMax,Step5mHMin,Step5mHMax,Step6mHMin,Step6mHMax,ValveControlTypeId,ValveFuelTypeId")] Valve valve)
        {
            if (ModelState.IsValid)
            {
                db.Entry(valve).State = EntityState.Modified;
                db.SaveChanges();
                // 2016-03-10 LCJ Changed to return to the Part index view instead of the Valve index view
                return RedirectToAction("Index","Parts");
            }
            ViewBag.PartId = new SelectList(db.Parts, "PartId", "PartNumber", valve.PartId);
            ViewBag.ValveControlTypeId = new SelectList(db.ValveControlTypes, "ValveControlTypeId", "ValveControlTypeDesc", valve.ValveControlTypeId);
            ViewBag.ValveFuelTypeId = new SelectList(db.ValveFuelTypes, "ValveFuelTypeId", "ValveFuelTypeDesc", valve.ValveFuelTypeId);
            return View(valve);
        }

        // GET: Valves/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Valve valve = db.Valves.Find(id);
            if (valve == null)
            {
                return HttpNotFound();
            }
            return View(valve);
        }

        // POST: Valves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Valve valve = db.Valves.Find(id);
            db.Valves.Remove(valve);
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
