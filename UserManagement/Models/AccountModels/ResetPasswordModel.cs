
namespace UserManagement.Models.AccountModels
{
    using System.ComponentModel.DataAnnotations;

    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "ResetToken is required.")]
        public string ResetToken { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string NewPassword { get; set; }
    }
}
