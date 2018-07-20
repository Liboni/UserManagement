
namespace UserManagement.Models.JobModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class AddJobModel
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Talent is required.")]
        public int TalentId { get; set; }
        [Required(ErrorMessage = "Country is required.")]
        public int CountryId { get; set; }
        public string Compensation { get; set; }
        public byte Gender { get; set; }
        public IFormFile ProfileImage { get; set; }
        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }
        public DateTime DueDate { get; set; }
    }
}
