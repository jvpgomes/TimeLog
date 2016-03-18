using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using JG.TimeLog.Web.Models;
using JG.TimeLog.Web.DataAccess;

namespace JG.TimeLog.Web.Controllers.ReportControllers
{
    public class TimePerProjectsController : Controller
    {
        private LocalMsSqlDb db = new LocalMsSqlDb();

        private IEnumerable<TimePerProject> GetTimePerProject(IEnumerable<TimeEntry> TotalTime)
        {
            return (from entry in TotalTime
                    group entry by entry.Project into g
                    select 
                    new TimePerProject
                    {
                        Project = g.Key,
                        TotalTime = g.Sum(e => e.Hours),
                        Customer = g.Key.Customer
                    }
                );
        }

        // GET: TimePerProject
        public ActionResult IndexTotal()
        {
            return View(GetTimePerProject(db.GetTimeEntriesList()));
        }

        // GET: TimePerProject for the logged user
        public ActionResult Index()
        {
            return View(GetTimePerProject(db.GetTimeEntriesListPerUser(User.Identity.Name)));
        }
    }
}
