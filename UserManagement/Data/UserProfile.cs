
namespace UserManagement.Data
{
    using System;

    using UserManagement.Enums;
    using UserManagement.Models;

    public partial class UserProfile
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
         public string LastName { get; set; }
        public byte Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int CountryId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Country Country { get; set; }
    }
}
