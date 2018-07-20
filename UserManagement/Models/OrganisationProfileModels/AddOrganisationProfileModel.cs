
namespace UserManagement.Models.OrganisationProfileModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class AddOrganisationProfileModel
    {
        [Required(ErrorMessage = "Company name is required."), MinLength(2, ErrorMessage = "Enter a valid company name")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Company registration ID is required."), MinLength(2, ErrorMessage = "Enter a valid company registration ID")]
        public string CompanyRegistrationId { get; set; }

        [Required(ErrorMessage = "Company business type is required."), Range(1, Int32.MaxValue, ErrorMessage = "Enter a valid country")]
        public int CompanyBusinessType { get; set; }

        [Required(ErrorMessage = "Date of company registration  is required.")]
        public DateTime DateOfCompanyRegistration { get; set; }

        [Required(ErrorMessage = "Company address is required.")]
        public string CompanyAddress { get; set; }

        [Required(ErrorMessage = "Company phonenumber is required.")]
        public string CompanyPhoneNumber { get; set; }

        [Required(ErrorMessage = "Country is required."), Range(1,250, ErrorMessage = "Enter a valid country")]
        public int CountryId { get; set; }
        public IFormFile ProfileImage { get; set; }
    }
}
