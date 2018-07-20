
namespace UserManagement.Models.AccountModels
{
    using System;
    using System.Collections.Generic;

    public class UserModel
    {
        public string UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsConfirmed { get; set; }
        public string UserName { get; set; }
        public IList<string> Roles { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
