using System.Net;
using System.Web.Mvc;
using JG.TimeLog.Web.Models;
using JG.TimeLog.Web.DataAccess;

namespace JG.TimeLog.Web.Controllers
{
    public class ProjectsController : Controller
    {
        private LocalMsSqlDb db = new LocalMsSqlDb();

        // GET: Projects
        public ActionResult Index()
        {
            return View(db.GetProjectsList());
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.SelectProjectFromId(id.Value);
            if (project == null)
            {
                return HttpNotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.GetCustomersList(), "Id", "Name");
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,CustomerId")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.InsertProject(project);
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.GetCustomersList(), "Id", "Name", project.CustomerId);
            return View(project);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.SelectProjectFromId(id.Value);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.GetCustomersList(), "Id", "Name", project.CustomerId);
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,CustomerId")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.EditProject(project);
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.GetCustomersList(), "Id", "Name", project.CustomerId);
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.SelectProjectFromId(id.Value);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.DeleteProject(id);
            return RedirectToAction("Index");
        }
    }
}
