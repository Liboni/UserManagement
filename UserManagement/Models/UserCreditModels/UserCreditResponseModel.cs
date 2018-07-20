
namespace UserManagement.Models.UserCreditModels
{
    using System;

    using UserManagement.Data;

    public class UserCreditResponseModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public string Production { get; set; }
        public Talent Talent { get; set; }
    }
}
