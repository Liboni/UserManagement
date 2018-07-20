
namespace UserManagement.Models.JobApplicationModels
{
    using System.ComponentModel.DataAnnotations;

    public class AddJobApplicationModel
    {
        [Required(ErrorMessage = "Job is required.")]
        public int JobId { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        public string UserId { get; set; }
    }
}
