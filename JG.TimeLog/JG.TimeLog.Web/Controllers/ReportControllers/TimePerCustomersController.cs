using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using JG.TimeLog.Web.Models;
using JG.TimeLog.Web.DataAccess;

namespace JG.TimeLog.Web.Controllers.ReportControllers
{
    public class TimePerCustomersController : Controller
    {
        private LocalMsSqlDb db = new LocalMsSqlDb();

        private IEnumerable<TimePerCustomer> GetTimePerCustomer(IEnumerable<TimeEntry> TotalTime)
        {
            return (from entry in TotalTime
                    group entry by entry.Project.Customer into g
                    select
                    new TimePerCustomer
                    {
                        Customer = g.Key,
                        TotalTime = g.Sum(e => e.Hours)
                    }
                );
        }

        // GET: TimePerCustomer
        public ActionResult IndexTotal()
        {
            return View(GetTimePerCustomer(db.GetTimeEntriesList()));
        }

        // GET: TimePerCustomer for the logged user
        public ActionResult Index()
        {
            return View(GetTimePerCustomer(db.GetTimeEntriesListPerUser(User.Identity.Name)));
        }
    }
}
