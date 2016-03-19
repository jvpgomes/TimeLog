using System;
using System.Net;
using System.Web.Mvc;
using JG.TimeLog.Web.Models;
using JG.TimeLog.Web.DataAccess;

namespace JG.TimeLog.Web.Controllers
{
    public class TimeEntriesController : Controller
    {
        private LocalMsSqlDb db = new LocalMsSqlDb();

        // GET: TimeEntries
        public ActionResult Index()
        {
            return View(db.GetTimeEntriesListPerUser(User.Identity.Name));
        }

        // GET: TimeEntries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeEntry timeEntry = db.SelectTimeEntryFromId(id.Value);
            if (timeEntry == null)
            {
                return HttpNotFound();
            }
            return View(timeEntry);
        }

        // GET: TimeEntries/Create
        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(db.GetProjectsList(), "Id", "Name");
            return View();
        }

        // POST: TimeEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Username,ProjectId,Date,Hours,AddedDateTime,LastUpdatedDateTime")] TimeEntry timeEntry)
        {
            if (ModelState.IsValid)
            {
                var currDateTime = DateTimeOffset.Now;
                timeEntry.AddedDateTime = currDateTime;
                timeEntry.LastUpdatedDateTime = currDateTime;
                timeEntry.Username = User.Identity.Name;
                db.InsertTimeEntry(timeEntry);
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(db.GetProjectsList(), "Id", "Name", timeEntry.ProjectId);
            return View(timeEntry);
        }

        // GET: TimeEntries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeEntry timeEntry = db.SelectTimeEntryFromId(id.Value);
            if (timeEntry == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.GetProjectsList(), "Id", "Name", timeEntry.ProjectId);
            return View(timeEntry);
        }

        // POST: TimeEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,ProjectId,Date,Hours,AddedDateTime,LastUpdatedDateTime")] TimeEntry timeEntry)
        {
            if (ModelState.IsValid)
            {
                timeEntry.LastUpdatedDateTime = DateTimeOffset.Now;
                db.EditTimeEntry(timeEntry);
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.GetProjectsList(), "Id", "Name", timeEntry.ProjectId);
            return View(timeEntry);
        }

        // GET: TimeEntries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeEntry timeEntry = db.SelectTimeEntryFromId(id.Value);
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
            db.DeleteTimeEntry(id);
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
