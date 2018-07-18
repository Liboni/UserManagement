
namespace UserManagement.Models.JobModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    using UserManagement.Enums;

    public class JobModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "User ID is required.")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "Job name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Job description is required.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Talent is required.")]
        public int TalentId { get; set; }
        public int CountryId { get; set; }
        public string Compensation { get; set; }
        public byte Gender { get; set; }
        public IFormFile ProfileImage { get; set; }
        public string Address { get; set; }
        public DateTime DueDate { get; set; }
        public bool Disabled { get; set; }
    }
}
