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
        [DataType(DataType.Date), DisplayName("Start (Datetime)"), DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTimeOffset StartDateTime { get; set; }
        [DataType(DataType.Date), DisplayName("End (Datetime)")]
        public DateTimeOffset EndDateTime { get; set; }
        [DataType(DataType.DateTime), DisplayName("Added (Datetime)")]
        public DateTimeOffset AddedDateTime { get; set; }
        [DataType(DataType.DateTime), DisplayName("Updated (Datetime)")]
        public DateTimeOffset LastUpdatedDateTime { get; set; }
    }
}