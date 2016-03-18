using System.ComponentModel;

namespace JG.TimeLog.Web.Models
{
    public class TimePerProject
    {
        public Project Project { get; set; }
        [DisplayName("Number of Hours")]
        public long TotalTime { get; set; }
        public Customer Customer { get; set; }
    }
}