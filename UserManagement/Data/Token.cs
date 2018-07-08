
namespace UserManagement.Data
{
    using System;

    using UserManagement.Models;

    public partial class Token
    {
        public string Id { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string UserToken { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
