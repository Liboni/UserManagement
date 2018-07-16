
namespace UserManagement.Models.UserProfileModels
{
    using System;

    using UserManagement.Data;

    public class UserProfileResponseModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImageName { get; set; }
        public byte GenderId { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int CountryId { get; set; }
        public DateTime DateCreated { get; set; }
        public ApplicationUser User { get; set; }
        public Country Country { get; set; }
    }
}
