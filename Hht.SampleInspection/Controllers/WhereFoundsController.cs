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
    public class WhereFoundsController : Controller
    {
        private SampleInspectionEntities db = new SampleInspectionEntities();

        // GET: WhereFounds
        public ActionResult Index()
        {
            return View(db.WhereFounds.ToList());
        }

        // GET: WhereFounds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WhereFound whereFound = db.WhereFounds.Find(id);
            if (whereFound == null)
            {
                return HttpNotFound();
            }
            return View(whereFound);
        }

        // GET: WhereFounds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WhereFounds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WhereFoundId,WhereFoundDesc")] WhereFound whereFound)
        {
            if (ModelState.IsValid)
            {
                db.WhereFounds.Add(whereFound);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(whereFound);
        }

        // GET: WhereFounds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WhereFound whereFound = db.WhereFounds.Find(id);
            if (whereFound == null)
            {
                return HttpNotFound();
            }
            return View(whereFound);
        }

        // POST: WhereFounds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WhereFoundId,WhereFoundDesc")] WhereFound whereFound)
        {
            if (ModelState.IsValid)
            {
                db.Entry(whereFound).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(whereFound);
        }

        // GET: WhereFounds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WhereFound whereFound = db.WhereFounds.Find(id);
            if (whereFound == null)
            {
                return HttpNotFound();
            }
            // 2016-03-10 LCJ Test if WhereFound is still in use
            bool exists = db.WhereFounds.Any(w => w.PartReceiveds.Any(s => s.WhereFoundId == id));
            if (exists)
            {
                return View("HttGeneralError", new HandleErrorInfo(new Exception("Where Found cannot be deleted because is still in use."), "WhereFounds", "Index"));
            }
            return View(whereFound);
        }

        // POST: WhereFounds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WhereFound whereFound = db.WhereFounds.Find(id);
            db.WhereFounds.Remove(whereFound);
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
