
namespace UserManagement.Data
{
    using System;

    using UserManagement.Models;

    public class Job
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TalentId { get; set; }
        public string Compensation { get; set; }
        public string Location { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual Talent Talent { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
