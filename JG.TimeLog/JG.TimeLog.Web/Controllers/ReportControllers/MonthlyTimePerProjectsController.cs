using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using JG.TimeLog.Web.Models;
using JG.TimeLog.Web.DataAccess;

namespace JG.TimeLog.Web.Controllers.ReportControllers
{
    public class MonthlyTimePerProjectsController : Controller
    {
        private LocalMsSqlDb db = new LocalMsSqlDb();

        private IEnumerable<MonthlyTimePerProject> GetMonthlyTimePerProject(IEnumerable<TimeEntry> TotalTime)
        {
            return (from entry in TotalTime
                    group entry by new { p = entry.Project, m = entry.Date.Month, y = entry.Date.Year } into g
                    select
                    new MonthlyTimePerProject
                    {
                        Year = g.Key.y,
                        Month = g.Key.m,
                        Project = g.Key.p,
                        TotalTime = g.Sum(e => e.Hours),
                        Customer = g.Key.p.Customer
                    }
                );
        }

        // GET: MonthlyTimePerProject
        public ActionResult IndexTotal()
        {
            return View(GetMonthlyTimePerProject(db.GetTimeEntriesList()));
        }

        // GET: MonthlyTimePerProject for the logged user
        public ActionResult Index()
        {
            return View(GetMonthlyTimePerProject(db.GetTimeEntriesListPerUser(User.Identity.Name)));
        }
    }
}
