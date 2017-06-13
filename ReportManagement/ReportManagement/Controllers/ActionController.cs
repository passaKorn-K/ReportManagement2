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
    public class ActionController : Controller
    {
        private ReportEntities db = new ReportEntities();

        // GET: Action
        public ActionResult Index()
        {
            var actions = db.Actions.Include(a => a.Report).Include(a => a.User);
            return View(actions.ToList());
        }

        // GET: Action/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Action action = db.Actions.Find(id);
            if (action == null)
            {
                return HttpNotFound();
            }
            return View(action);
        }

        // GET: Action/Create
        public ActionResult Create()
        {
            ViewBag.ReportID = new SelectList(db.Reports, "ReportID", "ReportName");
            ViewBag.ActionUserID = new SelectList(db.Users, "userID", "firstName");
            return View();
        }

        // POST: Action/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ActionID,ActionName,ActionUserID,ActionDate,ReportID")] Action action)
        {
            if (ModelState.IsValid)
            {
                db.Actions.Add(action);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ReportID = new SelectList(db.Reports, "ReportID", "ReportName", action.ReportID);
            ViewBag.ActionUserID = new SelectList(db.Users, "userID", "firstName", action.ActionUserID);
            return View(action);
        }

        // GET: Action/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Action action = db.Actions.Find(id);
            if (action == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReportID = new SelectList(db.Reports, "ReportID", "ReportName", action.ReportID);
            ViewBag.ActionUserID = new SelectList(db.Users, "userID", "firstName", action.ActionUserID);
            return View(action);
        }

        // POST: Action/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ActionID,ActionName,ActionUserID,ActionDate,ReportID")] Action action)
        {
            if (ModelState.IsValid)
            {
                db.Entry(action).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReportID = new SelectList(db.Reports, "ReportID", "ReportName", action.ReportID);
            ViewBag.ActionUserID = new SelectList(db.Users, "userID", "firstName", action.ActionUserID);
            return View(action);
        }

        // GET: Action/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Action action = db.Actions.Find(id);
            if (action == null)
            {
                return HttpNotFound();
            }
            return View(action);
        }

        // POST: Action/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Action action = db.Actions.Find(id);
            db.Actions.Remove(action);
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
