
namespace UserManagement
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;

    using UserManagement.Data;
    using UserManagement.Enums;
    using UserManagement.Models;
    using UserManagement.Models.UserProfileModels;

    public class ObjectConverter
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        public ObjectConverter(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<UserProfileResponseModel> ToUserProfileResponseModel(UserProfile userProfile, string webRootPath)
        {
            ApplicationUser user = await userManager.FindByIdAsync(userProfile.UserId);
            return new UserProfileResponseModel
            {
                           UserId = userProfile.UserId,
                           Id = userProfile.Id,
                           ProfileImageName = Convert.ToBase64String(File.ReadAllBytes(Path.Combine(webRootPath, userProfile.ProfileImageName))),
                           Gender = ((Gender)userProfile.Gender).ToString(),
                           GenderId = userProfile.Gender,
                           DateOfBirth = userProfile.DateOfBirth,
                           FirstName = userProfile.FirstName,
                           CountryId = userProfile.CountryId,
                           LastName = userProfile.LastName,
                           Country = context.Countries.Find(userProfile.CountryId),
                           User =new ApplicationUser
                                     {
                                         DateCreated = user.DateCreated,
                                         Id = user.Id,
                                         Email = user.Email,
                                         UserName = user.UserName,
                                         PhoneNumber = user.PhoneNumber
                           } ,
                           DateCreated = userProfile.DateCreated
            };
        }
    }
}
