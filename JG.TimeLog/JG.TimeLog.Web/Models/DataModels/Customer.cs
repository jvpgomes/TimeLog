using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JG.TimeLog.Web.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        [DataType(DataType.PostalCode), DisplayName("Postal Code")]
        public string PostalCode { get; set; }
        public string Country { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}