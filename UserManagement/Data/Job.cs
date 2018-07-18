
namespace UserManagement.Data
{
    using System;

    using UserManagement.Enums;
    using UserManagement.Models;

    public class Job
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProfileImageName { get; set; }
        public int TalentId { get; set; }
        public DateTime DueDate { get; set; }
        public bool Disabled { get; set; }
        public string Compensation { get; set; }
        public byte Gender { get; set; }
        public string Address { get; set; }
        public int CountryId { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual Talent Talent { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Country Country { get; set; }
    }
}
