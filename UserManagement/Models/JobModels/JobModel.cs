
namespace UserManagement.Models.JobModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class JobModel:AddJobModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "User ID is required.")]
        public string UserId { get; set; }
        public bool Disabled { get; set; }
    }
}
