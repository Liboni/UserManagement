
namespace UserManagement.Models.JobModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

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
        public string Compensation { get; set; }
        public Gender Gender { get; set; }
        public string Location { get; set; }
        public DateTime DueDate { get; set; }
        public bool Disabled { get; set; }
    }
}
