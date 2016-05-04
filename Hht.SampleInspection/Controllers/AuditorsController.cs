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
    public class AuditorsController : Controller
    {
        private SampleInspectionEntities db = new SampleInspectionEntities();

        // GET: Auditors
        public ActionResult Index()
        {
            return View(db.Auditors.ToList());
        }

        // GET: Auditors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Auditor auditor = db.Auditors.Find(id);
            if (auditor == null)
            {
                return HttpNotFound();
            }
            return View(auditor);
        }

        // GET: Auditors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Auditors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AuditorId,AuditorName")] Auditor auditor)
        {
            if (ModelState.IsValid)
            {
                db.Auditors.Add(auditor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(auditor);
        }

        // GET: Auditors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Auditor auditor = db.Auditors.Find(id);
            if (auditor == null)
            {
                return HttpNotFound();
            }
            return View(auditor);
        }

        // POST: Auditors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AuditorId,AuditorName")] Auditor auditor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(auditor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(auditor);
        }

        // GET: Auditors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Auditor auditor = db.Auditors.Find(id);
            if (auditor == null)
            {
                return HttpNotFound();
            }
            // 2016-03-10 LCJ Test if Auditor is still in use
            bool exists = db.Auditors.Any(w => w.PartReceiveds.Any(s => s.AuditorId == id));
            if(exists)
            {
                return View("HttpGeneralError", new HandleErrorInfo(new Exception("Auditor cannot be deleted because is still in use."),"Auditors","Index")); 
            }
            return View(auditor);
        }

        // POST: Auditors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Auditor auditor = db.Auditors.Find(id);
            db.Auditors.Remove(auditor);
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
