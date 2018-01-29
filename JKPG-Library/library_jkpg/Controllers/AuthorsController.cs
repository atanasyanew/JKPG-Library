using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using library_jkpg.Models;
using PagedList;

namespace library_jkpg.Controllers
{
    [Authorize]
    public class AuthorsController : Controller
    {
        private MyDbContext db = new MyDbContext();


        // GET: Authors
        //public ActionResult Index()
        //{
        //    return View(db.AUTHORs.ToList());
        //}
        public ActionResult Index(string sortOrder,
                                  string currentFilter,
                                  string searchString,
                                  int? page,
                                  int? show)
        {
            //sort strings
            ViewBag.CurrentSort = sortOrder;
            ViewBag.FirstName = sortOrder == "FirstName" ? "FirstName_desc" : "FirstName";
            ViewBag.LastName = sortOrder == "LastName" ? "LastName_desc" : "LastName";
            ViewBag.BirthYear = sortOrder == "BirthYear" ? "BirthYear_desc" : "BirthYear";

            //Find by...
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var authors = from a in db.AUTHORs
                        select a;

            //Find by Title, ISBN or Publication Info
            if (!String.IsNullOrEmpty(searchString))
            {
                authors = authors.Where(a => a.FirstName.Contains(searchString)
                                      || a.LastName.Contains(searchString)
                                      || a.BirthYear.Contains(searchString)
                                      );
            }

            switch (sortOrder)
            {
                case "FirstName":
                    authors = authors.OrderBy(a => a.FirstName);
                    break;
                case "FirstName_desc":
                    authors = authors.OrderByDescending(a => a.FirstName);
                    break;
                case "LastName":
                    authors = authors.OrderBy(a => a.LastName);
                    break;
                case "LastName_desc":
                    authors = authors.OrderByDescending(a => a.LastName);
                    break;
                case "BirthYear":
                    authors = authors.OrderBy(a=> a.BirthYear);
                    break;
                case "BirthYear_desc":
                    authors = authors.OrderByDescending(a => a.BirthYear);
                    break;
                default:
                    authors = authors.OrderByDescending(a => a.Aid);
                    break;
            }

            // return View(books.ToList());
            //int pageSize = 20;
            int pageSize = (show ?? 20);
            int pageNumber = (page ?? 1);
            return View(authors.ToPagedList(pageNumber, pageSize));
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
        [Authorize(Roles = "Administrator, Librarian")]
        public ActionResult Create()
        {
            return View();
        }


        // POST: Authors/Create
        [Authorize(Roles = "Administrator, Librarian")]
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
        [Authorize(Roles = "Administrator, Librarian")]
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
        [Authorize(Roles = "Administrator, Librarian")]
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
        [Authorize(Roles = "Administrator, Librarian")]
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
        [Authorize(Roles = "Administrator, Librarian")]
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
