using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JG.TimeLog.Web.Models
{
    public class TimeEntry
    {
        public int Id { get; set; }
        [DisplayName("User")]
        public string Username { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [DisplayName("Number of hours")]
        public int Hours { get; set; }
        [DataType(DataType.DateTime), DisplayName("Added on")]
        public DateTimeOffset AddedDateTime { get; set; }
        [DataType(DataType.DateTime), DisplayName("Updated on")]
        public DateTimeOffset LastUpdatedDateTime { get; set; }
    }
}