
namespace UserManagement.BusinessLogics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    using UserManagement.Data;
    using UserManagement.LocalObjects;

    public class ProfileImagesManager
    {
        private readonly ApplicationDbContext context;

        public ProfileImagesManager(ApplicationDbContext context)
        {
            this.context = context;
        }

        public GenericActionResult<string> UpdateProfileImage(ProfileImage profileImage)
        {
            try
            {
                ProfileImage image = context.ProfileImages.FirstOrDefault(a => a.UserId.Equals(profileImage.UserId));
                if (image != null) {
                    image.Location = profileImage.Location;
                    image.Name = profileImage.Name;
                }
                context.SaveChanges();
                return new GenericActionResult<string>(true,"");
            }
            catch (Exception exception)
            {
                return new GenericActionResult<string>(exception.Message);
            }
        }

        public GenericActionResult<string> SaveProfileImage(ProfileImage profileImage)
        {
            try
            {
                context.ProfileImages.Add(profileImage);
                context.SaveChanges();
                return new GenericActionResult<string>(true, "");
            }
            catch (Exception exception)
            {
                return new GenericActionResult<string>(exception.Message);
            }
        }

        public GenericActionResult<ProfileImage> GetProfileImageByUserId(string userId)
        {
            try
            {
                return string.IsNullOrEmpty(userId) ? new GenericActionResult<ProfileImage>("User ID is required") : GenericActionResult<ProfileImage>.FromObject(context.ProfileImages.FirstOrDefault(a => a.UserId.Equals(userId) && !a.IsDeleted));
            }
            catch (Exception exception)
            {
                return new GenericActionResult<ProfileImage>(exception.Message);
            }
        }

        public GenericActionResult<List<ProfileImage>> GetProfileImages()
        {
            try
            {
                return GenericActionResult<List<ProfileImage>>.FromObject(context.ProfileImages.Where(a=> !a.IsDeleted).ToList());
            }
            catch (Exception exception)
            {
                return new GenericActionResult<List<ProfileImage>>(exception.Message);
            }
        }

        public GenericActionResult<string> DeleteProfileImage(int imageId)
        {
            try
            {
                ProfileImage profileImage = context.ProfileImages.Find(imageId);
                profileImage.IsDeleted = true;
                return new GenericActionResult<string>(true,"");
            }
            catch (Exception exception)
            {
                return new GenericActionResult<string>(exception.Message);
            }
        }
    }
}
