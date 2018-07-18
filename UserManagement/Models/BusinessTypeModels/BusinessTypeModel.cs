
namespace UserManagement.Models.BusinessTypeModels
{
    using System.ComponentModel.DataAnnotations;

    public class BusinessTypeModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Business type name is required")]
        public string Name { get; set; }
    }
}
