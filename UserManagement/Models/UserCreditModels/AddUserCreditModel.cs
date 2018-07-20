
namespace UserManagement.Models.UserCreditModels
{
    using System.ComponentModel.DataAnnotations;

    public class AddUserCreditModel
    {
        [Required(ErrorMessage = "Production is required.")]
        public string Production { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public int TalentId { get; set; }
    }
}
