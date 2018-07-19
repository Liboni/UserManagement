
namespace UserManagement.Models.UserCreditModels
{
    using System;

    using UserManagement.Data;

    public class UserCreditResponseModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public UserProfile UserProfile { get; set; }
        public string Production { get; set; }
        public Talent Talent { get; set; }
    }
}
