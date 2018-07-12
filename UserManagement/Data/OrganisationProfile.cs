using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Data
{
    using UserManagement.Models;

    public class OrganisationProfile
    {
        public int Id { get; set; }
         public string CompanyName { get; set; }
         public string UserId { get; set; }
         public string CompanyRegistrationId { get; set; }
         public int CompanyBusinessType { get; set; }
         public DateTime DateOfCompanyRegistration { get; set; }
         public string CompanyAddress { get; set; }
         public string CompanyPhoneNumber { get; set; }
         public int CountryId { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Country Country { get; set; }
    }
}
