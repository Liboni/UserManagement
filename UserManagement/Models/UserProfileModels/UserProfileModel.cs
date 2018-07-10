
namespace UserManagement.Models.UserProfileModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using UserManagement.Enums;

    public class UserProfileModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "User Id is required.")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "First name is required."), MinLength(2, ErrorMessage = "Enter a valid first name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required."), MinLength(2,ErrorMessage = "Enter a valid last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Date of birth is required.")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Country is required."),Range(1,Int32.MaxValue,ErrorMessage = "Enter a valid country")]
        public int CountryId { get; set; }

    }
}
