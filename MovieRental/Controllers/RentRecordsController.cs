using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieRental;

namespace MovieRental.Controllers
{
    public class RentRecordsController : Controller
    {
        private MoviesEntities db = new MoviesEntities();

        // GET: RentRecords
        public ActionResult Index()
        {
            var rentRecords = db.RentRecords.Include(r => r.Customer).Include(r => r.Movie);
            return View(rentRecords.ToList());
        }

        // GET: RentRecords/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentRecord rentRecord = db.RentRecords.Find(id);
            if (rentRecord == null)
            {
                return HttpNotFound();
            }
            return View(rentRecord);
        }

        // GET: RentRecords/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName");
            ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "MovieName");
            return View();
        }

        // POST: RentRecords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RentalID,MovieID,CustomerID,RentalDate,DueDate,ReturnDate")] RentRecord rentRecord)
        {
            if (ModelState.IsValid)
            {
                db.RentRecords.Add(rentRecord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName", rentRecord.CustomerID);
            ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "MovieName", rentRecord.MovieID);
            return View(rentRecord);
        }

        // GET: RentRecords/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentRecord rentRecord = db.RentRecords.Find(id);
            if (rentRecord == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName", rentRecord.CustomerID);
            ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "MovieName", rentRecord.MovieID);
            return View(rentRecord);
        }

        // POST: RentRecords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RentalID,MovieID,CustomerID,RentalDate,DueDate,ReturnDate")] RentRecord rentRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rentRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName", rentRecord.CustomerID);
            ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "MovieName", rentRecord.MovieID);
            return View(rentRecord);
        }

        // GET: RentRecords/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentRecord rentRecord = db.RentRecords.Find(id);
            if (rentRecord == null)
            {
                return HttpNotFound();
            }
            return View(rentRecord);
        }

        // POST: RentRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RentRecord rentRecord = db.RentRecords.Find(id);
            db.RentRecords.Remove(rentRecord);
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
