
namespace UserManagement.Models.UserProfileModels
{
    using System;

    using UserManagement.Data;
    using UserManagement.Models.ValuesModels;

    public class UserProfileResponseModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImage { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public GenderModel Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int CountryId { get; set; }
        public DateTime DateCreated { get; set; }
        public Country Country { get; set; }
    }
}
