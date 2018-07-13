
namespace UserManagement.Models.UserCreditModels
{
    using System.ComponentModel.DataAnnotations;

    public class UserCreditModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Production is required.")]
        public string Production { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public int TalentId { get; set; }
    }
}
