
namespace UserManagement.Models.TalentModels
{
    using System.ComponentModel.DataAnnotations;

    public class TalentModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Talent name is required")]
        public string Name { get; set; }
    }
}
