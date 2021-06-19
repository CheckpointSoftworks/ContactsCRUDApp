using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactsCRUDApp.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhonePrimary { get; set; }
        public string PhoneSecondary { get; set; }
        public DateTime Birthday { get; set; }
        public string StressAddress1 { get; set; }
        public string StressAddress2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}