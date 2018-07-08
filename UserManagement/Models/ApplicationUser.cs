
namespace UserManagement.Models
{
    using System;

    using Microsoft.AspNetCore.Identity;
    public class ApplicationUser : IdentityUser
    {
        public DateTime DateCreated { get; set; }
    }
}
