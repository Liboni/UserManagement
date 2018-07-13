using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Models.JobApplicationModels
{
    using System.ComponentModel.DataAnnotations;

    public class JobApplicationModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Job is required.")]
        public int JobId { get; set; }
        [Required(ErrorMessage = "User ID is required.")]
        public string UserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
