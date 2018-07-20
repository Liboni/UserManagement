
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

        public async Task<GenericActionResult<UserProfile>> SaveUserDetails(UserProfileModel userProfileModel, string webRootPath)
        {
            try
            {
                if(context.UserProfiles.FirstOrDefault(a => a.UserId.Equals(userProfileModel.UserId))!=null)
                    return await UpdateUserDetails(userProfileModel, webRootPath);
                var userProfile = new UserProfile
                            {
                                CountryId = userProfileModel.CountryId,
                                DateOfBirth = userProfileModel.DateOfBirth,
                                FirstName = userProfileModel.FirstName,
                                Gender = (byte)userProfileModel.Gender,
                                LastName = userProfileModel.LastName,
                                UserId = userProfileModel.UserId,
                                DateCreated = DateTime.Now,
                                ProfileImageName =await UploadFile.SaveFileInWebRoot(userProfileModel.ProfileImage,webRootPath)
                            };
                context.UserProfiles.Add(userProfile);
                context.SaveChanges();
                return new GenericActionResult<UserProfile>(true,"User details saved successfully.",userProfile);
            }
            catch (Exception exception)
            {
                return new GenericActionResult<UserProfile>(exception.Message);
            }
        }

        public async Task<GenericActionResult<UserProfile>> UpdateUserDetails(UserProfileModel userProfileModel, string webRootPath)
        {
            try
            {
                UserProfile userProfile = context.UserProfiles.FirstOrDefault(a=>a.UserId.Equals(userProfileModel.UserId));
                if (userProfile != null) {
                    userProfile.CountryId = userProfileModel.CountryId;
                    userProfile.DateOfBirth = userProfileModel.DateOfBirth;
                    userProfile.FirstName = userProfileModel.FirstName;
                    userProfile.Gender = (byte)userProfileModel.Gender;
                    userProfile.LastName = userProfileModel.LastName;
                    userProfile.ProfileImageName = await UploadFile.SaveFileInWebRoot(userProfileModel.ProfileImage, webRootPath);
                }
                context.SaveChanges();
                return new GenericActionResult<UserProfile>(true, "User details updated successfully.",userProfile);
            }
            catch (Exception exception)
            {
                return new GenericActionResult<UserProfile>(exception.Message);
            }
        }

        public GenericActionResult<UserProfileResponseModel> GetUserDetailsByUserId(string userId, string webRootPath)
        {
            try
            {
                return new GenericActionResult<UserProfileResponseModel>(true,"", context.UserProfiles.Where(a => a.UserId.Equals(userId)).Select(a => new ObjectConverterManager(context, userManager).ToUserProfileResponseModel(a, webRootPath).Result).FirstOrDefault()); 
 
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
               return new GenericActionResult<List<UserProfileResponseModel>>(true, "", profiles.Select(userProfile => new ObjectConverterManager(context, userManager).ToUserProfileResponseModel(userProfile, webRootPath).Result).ToList());
            }
            catch (Exception exception)
            {
                return new GenericActionResult<List<UserProfileResponseModel>>(exception.Message);
            }
        }
    }
}
