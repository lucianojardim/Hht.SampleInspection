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
    public class PartCategoriesController : Controller
    {
        private SampleInspectionEntities db = new SampleInspectionEntities();

        // GET: PartCategories
        public ActionResult Index()
        {
            return View(db.PartCategories.ToList());
        }

        // GET: PartCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartCategory partCategory = db.PartCategories.Find(id);
            if (partCategory == null)
            {
                return HttpNotFound();
            }
            return View(partCategory);
        }

        // GET: PartCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PartCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PartCategoryId,PartCategoryDesc")] PartCategory partCategory)
        {
            if (ModelState.IsValid)
            {
                db.PartCategories.Add(partCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(partCategory);
        }

        // GET: PartCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartCategory partCategory = db.PartCategories.Find(id);
            if (partCategory == null)
            {
                return HttpNotFound();
            }
            return View(partCategory);
        }

        // POST: PartCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PartCategoryId,PartCategoryDesc")] PartCategory partCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(partCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(partCategory);
        }

        // GET: PartCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartCategory partCategory = db.PartCategories.Find(id);
            if (partCategory == null)
            {
                return HttpNotFound();
            }
            // 2016-03-10 LCJ Test if PartCategory is still in use
            bool exists = db.PartCategories.Any(w => w.Parts.Any(s => s.PartCategoryId == id));
            if (exists)
            {
                return View("HttGeneralError", new HandleErrorInfo(new Exception("Part Category cannot be deleted because is still in use."), "PartCategories", "Index"));
            }
            return View(partCategory);
        }

        // POST: PartCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PartCategory partCategory = db.PartCategories.Find(id);
            db.PartCategories.Remove(partCategory);
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
