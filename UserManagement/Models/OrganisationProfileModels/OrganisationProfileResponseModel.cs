
namespace UserManagement.Models.OrganisationProfileModels
{
    using System;

    using UserManagement.Data;

    public class OrganisationProfileResponseModel
    {
        public int Id { get; set; }
        public string ProfileImage { get; set; }
        public string CompanyName { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public string CompanyRegistrationId { get; set; }
        public BusinessType BusinessType { get; set; }
        public DateTime DateOfCompanyRegistration { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyPhoneNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public Country Country { get; set; }
    }
}
