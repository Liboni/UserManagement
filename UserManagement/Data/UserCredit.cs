
namespace UserManagement.Data
{
    using System;

    using UserManagement.Models;

    public class UserCredit
    {
        public int Id { get; set; }
        public string Production { get; set; }
        public string UserId { get; set; }
        public int RoleId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
