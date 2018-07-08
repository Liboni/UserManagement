
namespace UserManagement.Models
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;

    using UserManagement.Data;

    public class ApplicationUser : IdentityUser
    {
        public DateTime DateCreated { get; set; }

        public virtual ICollection<Token> RegistrationVerificationTokens { get; set; }

    }
}
