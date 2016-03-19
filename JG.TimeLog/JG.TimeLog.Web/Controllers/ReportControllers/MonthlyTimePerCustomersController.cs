using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using JG.TimeLog.Web.Models;
using JG.TimeLog.Web.DataAccess;

namespace JG.TimeLog.Web.Controllers
{
    public class MonthlyTimePerCustomersController : Controller
    {
        private LocalMsSqlDb db = new LocalMsSqlDb();

        private IEnumerable<MonthlyTimePerCustomer> GetMonthlyTimePerCustomer(IEnumerable<TimeEntry> TotalTime)
        {
            return (from entry in TotalTime
                    group entry by new { c = entry.Project.Customer, m = entry.Date.Month, y = entry.Date.Year } into g
                    select
                    new MonthlyTimePerCustomer
                    {
                        Year = g.Key.y,
                        Month = g.Key.m,
                        Customer = g.Key.c,
                        TotalTime = g.Sum(e => e.Hours)
                    }
                );
        }

        // GET: MonthlyTimePerCustomer
        public ActionResult IndexTotal()
        {
            return View(GetMonthlyTimePerCustomer(db.GetTimeEntriesList()));
        }

        // GET: MonthlyTimePerCustomer for the logged user
        public ActionResult Index()
        {
            return View(GetMonthlyTimePerCustomer(db.GetTimeEntriesListPerUser(User.Identity.Name)));
        }
    }
}
