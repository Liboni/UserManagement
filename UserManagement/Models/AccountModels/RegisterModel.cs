
namespace UserManagement.Models.AccountModels
{
    using System.ComponentModel.DataAnnotations;

    using UserManagement.Enums;

    public class RegisterModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required."), MinLength(6, ErrorMessage = "Required password minimun length is 6 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Phonenumber is required.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Register type is required.")]
        public RegisterType RegisterType { get; set; }
    }
}
