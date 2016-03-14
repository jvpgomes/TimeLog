using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JG.TimeLog.Web.Models;

namespace JG.TimeLog.Web.Controllers
{
    public class TimeEntriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TimeEntries
        public ActionResult Index()
        {
            var timeEntries = db.TimeEntries.Include(t => t.Project);
            return View(timeEntries.ToList());
        }

        // GET: TimeEntries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var timeEntries = db.TimeEntries.Include(p => p.Project);
            TimeEntry timeEntry = timeEntries.First(p => p.Id == id);
            if (timeEntry == null)
            {
                return HttpNotFound();
            }
            return View(timeEntry);
        }

        // GET: TimeEntries/Create
        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name");
            return View();
        }

        // POST: TimeEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Username,ProjectId,StartDateTime,EndDateTime,AddedDateTime,LastUpdatedDateTime")] TimeEntry timeEntry)
        {
            if (ModelState.IsValid)
            {
                var currDateTime = DateTimeOffset.Now;
                timeEntry.AddedDateTime = currDateTime;
                timeEntry.LastUpdatedDateTime = currDateTime;
                timeEntry.Username = User.Identity.Name;
                db.TimeEntries.Add(timeEntry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", timeEntry.ProjectId);
            return View(timeEntry);
        }

        // GET: TimeEntries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeEntry timeEntry = db.TimeEntries.Find(id);
            if (timeEntry == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", timeEntry.ProjectId);
            return View(timeEntry);
        }

        // POST: TimeEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,ProjectId,StartDateTime,EndDateTime,AddedDateTime,LastUpdatedDateTime")] TimeEntry timeEntry)
        {
            if (ModelState.IsValid)
            {
                timeEntry.LastUpdatedDateTime = DateTimeOffset.Now;
                db.Entry(timeEntry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", timeEntry.ProjectId);
            return View(timeEntry);
        }

        // GET: TimeEntries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var timeEntries = db.TimeEntries.Include(p => p.Project);
            TimeEntry timeEntry = timeEntries.First(p => p.Id == id);
            if (timeEntry == null)
            {
                return HttpNotFound();
            }
            return View(timeEntry);
        }

        // POST: TimeEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TimeEntry timeEntry = db.TimeEntries.Find(id);
            db.TimeEntries.Remove(timeEntry);
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
