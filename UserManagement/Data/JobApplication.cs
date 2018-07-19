
namespace UserManagement.Data
{
    using System;

    using UserManagement.Models;

    public class JobApplication
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string UserId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsViewed { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual Job Job { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
