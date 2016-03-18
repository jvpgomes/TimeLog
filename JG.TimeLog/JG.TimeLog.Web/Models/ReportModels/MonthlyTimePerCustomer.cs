using System.ComponentModel;

namespace JG.TimeLog.Web.Models
{
    public class MonthlyTimePerCustomer
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public Customer Customer { get; set; }
        [DisplayName("Number of Hours")]
        public int TotalTime { get; set; }
    }
}