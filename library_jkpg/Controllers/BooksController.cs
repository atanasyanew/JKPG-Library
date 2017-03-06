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
    [Authorize(Roles = "Administrator, Librarian")]
    public class BooksController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Books
        [Authorize]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, int? show)
        {
            //sort strings
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "Title_desc" : "";
            ViewBag.PublicationInfoSortParm = sortOrder == "Pinfo" ? "Pinfo_desc" : "Pinfo";
            ViewBag.IsbnSortParm = sortOrder == "Isbn" ? "Isbn_desc" : "Isbn";
            ViewBag.PagesSortParm = sortOrder == "Pages" ? "Pages_desc" : "Pages";
            ViewBag.SignumSortParm = sortOrder == "Signum" ? "Signum_desc" : "Signum";


            //Find by Title, ISBN or Publication Info
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var books = from b in db.BOOKs
                        select b;

            //Find by Title, ISBN or Publication Info
            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(b => b.Title.Contains(searchString)
                                      || b.ISBN.Contains(searchString)
                                      || b.publicationinfo.Contains(searchString)
                                      );
            }

            switch (sortOrder)
            {
                case "Title":
                    books = books.OrderBy(b => b.Title);
                    break;
                case "Title_desc":
                    books = books.OrderByDescending(b => b.Title);
                    break;
                case "Pinfo":
                    books = books.OrderBy(b => b.Title);
                    break;
                case "Pinfo_desc":
                    books = books.OrderByDescending(b => b.Title);
                    break;
                case "Isbn":
                    books = books.OrderBy(b => b.ISBN);
                    break;
                case "Isbn_desc":
                    books = books.OrderByDescending(b => b.ISBN);
                    break;
                case "Pages":
                    books = books.OrderBy(b => b.pages);
                    break;
                case "Pages_desc":
                    books = books.OrderByDescending(b => b.pages);
                    break;
                case "Signum":
                    books = books.OrderBy(b => b.SignId);
                    break;
                case "Signum_desc":
                    books = books.OrderByDescending(b => b.SignId);
                    break;
                default:
                    books = books.OrderBy(b => b.Title);
                    break;
            }

            // return View(books.ToList());
            //int pageSize = 20;
            int pageSize = (show ?? 20);
            int pageNumber = (page ?? 1);
            return View(books.ToPagedList(pageNumber, pageSize));
        }
        //original method
        //public ActionResult Index()
        //{
        //    var bOOKs = db.BOOKs.Include(b => b.CLASSIFICATION);
        //    return View(bOOKs.ToList());
        //}

        // GET: Books/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BOOK bOOK = db.BOOKs.Find(id);
            if (bOOK == null)
            {
                return HttpNotFound();
            }
            return View(bOOK);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            ViewBag.SignId = new SelectList(db.CLASSIFICATIONs, "SignId", "Signum");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ISBN,Title,SignId,PublicationYear,publicationinfo,pages")] BOOK bOOK)
        {
            if (ModelState.IsValid)
            {
                db.BOOKs.Add(bOOK);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SignId = new SelectList(db.CLASSIFICATIONs, "SignId", "Signum", bOOK.SignId);
            return View(bOOK);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BOOK bOOK = db.BOOKs.Find(id);
            if (bOOK == null)
            {
                return HttpNotFound();
            }
            ViewBag.SignId = new SelectList(db.CLASSIFICATIONs, "SignId", "Signum", bOOK.SignId);
            return View(bOOK);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ISBN,Title,SignId,PublicationYear,publicationinfo,pages")] BOOK bOOK)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bOOK).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SignId = new SelectList(db.CLASSIFICATIONs, "SignId", "Signum", bOOK.SignId);
            return View(bOOK);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BOOK bOOK = db.BOOKs.Find(id);
            if (bOOK == null)
            {
                return HttpNotFound();
            }
            return View(bOOK);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            BOOK bOOK = db.BOOKs.Find(id);
            db.BOOKs.Remove(bOOK);
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
