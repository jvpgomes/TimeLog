using System.ComponentModel;

namespace JG.TimeLog.Web.Models
{
    public class TimePerCustomer
    {
        public Customer Customer { get; set; }
        [DisplayName("Number of Hours")]
        public int TotalTime { get; set; }
    }
}