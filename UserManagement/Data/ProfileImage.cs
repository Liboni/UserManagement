
namespace UserManagement.Data
{
    using System;

    using UserManagement.Models;

    public partial class ProfileImage
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}
