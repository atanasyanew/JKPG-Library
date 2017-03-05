using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using library_jkpg.Models;

namespace library_jkpg.Controllers
{
    public class AuthorsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Authors
        public ActionResult Index()
        {
            return View(db.AUTHORs.ToList());
        }

        // GET: Authors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AUTHOR aUTHOR = db.AUTHORs.Find(id);
            if (aUTHOR == null)
            {
                return HttpNotFound();
            }
            return View(aUTHOR);
        }

        // GET: Authors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Aid,FirstName,LastName,BirthYear")] AUTHOR aUTHOR)
        {
            if (ModelState.IsValid)
            {
                db.AUTHORs.Add(aUTHOR);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aUTHOR);
        }

        // GET: Authors/Edit/5
        [Authorize(Roles ="Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AUTHOR aUTHOR = db.AUTHORs.Find(id);
            if (aUTHOR == null)
            {
                return HttpNotFound();
            }
            return View(aUTHOR);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Aid,FirstName,LastName,BirthYear")] AUTHOR aUTHOR)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aUTHOR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aUTHOR);
        }

        // GET: Authors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AUTHOR aUTHOR = db.AUTHORs.Find(id);
            if (aUTHOR == null)
            {
                return HttpNotFound();
            }
            return View(aUTHOR);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AUTHOR aUTHOR = db.AUTHORs.Find(id);
            db.AUTHORs.Remove(aUTHOR);
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
