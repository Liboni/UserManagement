
namespace UserManagement.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CreditsModel
    {
        [Required(ErrorMessage = "Production is required.")]
        public string Production { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public string Role { get; set; }
    }
}
