
namespace UserManagement.BusinessLogics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    using UserManagement.Data;
    using UserManagement.LocalObjects;
    using UserManagement.Models.UserProfileModels;

    public class UserProfileManager
    {
        private readonly ApplicationDbContext context;

        public UserProfileManager(ApplicationDbContext context)
        {
            this.context = context;
        }

        public GenericActionResult<string> SaveUserDetails(UserProfileModel userProfileModel)
        {
            try
            {
                context.UserProfiles.Add(
                    new UserProfile
                        {
                            CountryId = userProfileModel.CountryId,
                            DateOfBirth = userProfileModel.DateOfBirth,
                            FirstName = userProfileModel.FirstName,
                            Gender = (byte)userProfileModel.Gender,
                            LastName = userProfileModel.LastName,
                            UserId = userProfileModel.UserId
                        });
                context.SaveChanges();
                return new GenericActionResult<string>(true,"");
            }
            catch (Exception exception)
            {
                return new GenericActionResult<string>(exception.Message);
            }
        }

        public GenericActionResult<string> UpdateUserDetails(UserProfileModel userProfileModel)
        {
            try
            {
                UserProfile userProfile = context.UserProfiles.Find(userProfileModel.Id);
                userProfile.CountryId = userProfile.CountryId;
                userProfile.DateOfBirth = userProfile.DateOfBirth;
                userProfile.FirstName = userProfile.FirstName;
                userProfile.Gender = userProfile.Gender;
                userProfile.LastName = userProfile.LastName;
                context.SaveChanges();
                return new GenericActionResult<string>(true, "");
            }
            catch (Exception exception)
            {
                return new GenericActionResult<string>(exception.Message);
            }
        }

        public GenericActionResult<UserProfile> GetUserDetailsByUserId(string userId)
        {
            try
            {
                return new GenericActionResult<UserProfile>(true,"", context.UserProfiles.FirstOrDefault(a => a.UserId.Equals(userId))); 
 
            }
            catch (Exception exception)
            {
                return new GenericActionResult<UserProfile>(exception.Message);
            }
        }

        public GenericActionResult<List<UserProfile>> GetUserDetails()
        {
            try
            {
                return new GenericActionResult<List<UserProfile>>(true, "", context.UserProfiles.ToList());
            }
            catch (Exception exception)
            {
                return new GenericActionResult<List<UserProfile>>(exception.Message);
            }
        }
    }
}
