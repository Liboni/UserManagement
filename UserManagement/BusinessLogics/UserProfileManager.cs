
namespace UserManagement.BusinessLogics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;

    using UserManagement.Data;
    using UserManagement.LocalObjects;
    using UserManagement.Models;
    using UserManagement.Models.UserProfileModels;

    public class UserProfileManager
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        public UserProfileManager(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<GenericActionResult<string>> SaveUserDetails(UserProfileModel userProfileModel, string webRootPath)
        {
            try
            {
                if(context.UserProfiles.FirstOrDefault(a => a.UserId.Equals(userProfileModel.UserId))!=null)
                    return await UpdateUserDetails(userProfileModel, webRootPath);
                context.UserProfiles.Add(new UserProfile{
                            CountryId = userProfileModel.CountryId,
                            DateOfBirth = userProfileModel.DateOfBirth,
                            FirstName = userProfileModel.FirstName,
                            Gender = (byte)userProfileModel.Gender,
                            LastName = userProfileModel.LastName,
                            UserId = userProfileModel.UserId,
                            DateCreated = DateTime.Now,
                            ProfileImageName = await UploadFile.SaveFileInWebRoot(userProfileModel.ProfileImage, webRootPath)
                });
                context.SaveChanges();
                return new GenericActionResult<string>(true,"");
            }
            catch (Exception exception)
            {
                return new GenericActionResult<string>(exception.Message);
            }
        }

        public async Task<GenericActionResult<string>> UpdateUserDetails(UserProfileModel userProfileModel, string webRootPath)
        {
            try
            {
                UserProfile userProfile = context.UserProfiles.FirstOrDefault(a=>a.UserId.Equals(userProfileModel.UserId));
                if (userProfile != null) {
                    userProfile.CountryId = userProfile.CountryId;
                    userProfile.DateOfBirth = userProfile.DateOfBirth;
                    userProfile.FirstName = userProfile.FirstName;
                    userProfile.Gender = userProfile.Gender;
                    userProfile.LastName = userProfile.LastName;
                    userProfile.ProfileImageName = await UploadFile.SaveFileInWebRoot(userProfileModel.ProfileImage, webRootPath);
                }
                context.SaveChanges();
                return new GenericActionResult<string>(true, "User details updated successfully");
            }
            catch (Exception exception)
            {
                return new GenericActionResult<string>(exception.Message);
            }
        }

        public GenericActionResult<UserProfileResponseModel> GetUserDetailsByUserId(string userId, string webRootPath)
        {
            try
            {
                return new GenericActionResult<UserProfileResponseModel>(true,"", context.UserProfiles.Where(a => a.UserId.Equals(userId)).Select(a => new ObjectConverter(context, userManager).ToUserProfileResponseModel(a, webRootPath).Result).FirstOrDefault()); 
 
            }
            catch (Exception exception)
            {
                return new GenericActionResult<UserProfileResponseModel>(exception.Message);
            }
        }

        public GenericActionResult<List<UserProfileResponseModel>> GetAllUserDetails(string webRootPath, int from, int count)
        {
            try
            {
               List<UserProfile> profiles = context.UserProfiles.Skip(from).Take(count).ToList();
               return new GenericActionResult<List<UserProfileResponseModel>>(true, "", profiles.Select(userProfile => new ObjectConverter(context, userManager).ToUserProfileResponseModel(userProfile, webRootPath).Result).ToList());
            }
            catch (Exception exception)
            {
                return new GenericActionResult<List<UserProfileResponseModel>>(exception.Message);
            }
        }
    }
}
