using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReportManagement;

namespace ReportManagement.Controllers
{
    public class OpinionController : Controller
    {
        private ReportEntities db = new ReportEntities();

        // GET: Opinion
        public ActionResult Index()
        {
            var opinions = db.Opinions.Include(o => o.Report).Include(o => o.User);
            return View(opinions.ToList());
        }

        // GET: Opinion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Opinion opinion = db.Opinions.Find(id);
            if (opinion == null)
            {
                return HttpNotFound();
            }
            return View(opinion);
        }

        // GET: Opinion/Create
        public ActionResult Create()
        {
            ViewBag.ReportID = new SelectList(db.Reports, "ReportID", "ReportName");
            ViewBag.UserCreatedID = new SelectList(db.Users, "userID", "firstName");
            return View();
        }

        // POST: Opinion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OpinionID,ReportID,UserCreatedID,DateCreated,Opinion1")] Opinion opinion)
        {
            if (ModelState.IsValid)
            {
                db.Opinions.Add(opinion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ReportID = new SelectList(db.Reports, "ReportID", "ReportName", opinion.ReportID);
            ViewBag.UserCreatedID = new SelectList(db.Users, "userID", "firstName", opinion.UserCreatedID);
            return View(opinion);
        }

        // GET: Opinion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Opinion opinion = db.Opinions.Find(id);
            if (opinion == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReportID = new SelectList(db.Reports, "ReportID", "ReportName", opinion.ReportID);
            ViewBag.UserCreatedID = new SelectList(db.Users, "userID", "firstName", opinion.UserCreatedID);
            return View(opinion);
        }

        // POST: Opinion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OpinionID,ReportID,UserCreatedID,DateCreated,Opinion1")] Opinion opinion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(opinion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReportID = new SelectList(db.Reports, "ReportID", "ReportName", opinion.ReportID);
            ViewBag.UserCreatedID = new SelectList(db.Users, "userID", "firstName", opinion.UserCreatedID);
            return View(opinion);
        }

        // GET: Opinion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Opinion opinion = db.Opinions.Find(id);
            if (opinion == null)
            {
                return HttpNotFound();
            }
            return View(opinion);
        }

        // POST: Opinion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Opinion opinion = db.Opinions.Find(id);
            db.Opinions.Remove(opinion);
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
