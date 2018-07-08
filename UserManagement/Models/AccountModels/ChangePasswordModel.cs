
namespace UserManagement.Models.AccountModels
{
    using System.ComponentModel.DataAnnotations;

    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Old password is required.")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "New password is required.")]
        public string NewPassword { get; set; }
    }
}
