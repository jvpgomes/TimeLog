using System.Collections.Generic;

namespace JG.TimeLog.Web.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public virtual ICollection<TimeEntry> TimeEntries { get; set; }
    }
}