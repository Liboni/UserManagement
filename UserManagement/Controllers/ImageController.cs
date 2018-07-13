
namespace UserManagement.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using UserManagement.BusinessLogics;
    using UserManagement.Data;
    using UserManagement.LocalObjects;
    using UserManagement.Models.ImageModels;
    using UserManagement.Models.UserProfileModels;

    [Authorize]
    [Produces("application/json")]
    [Route("api/Image")]
    public class ImageController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IHostingEnvironment hostingEnvironment;

        public ImageController(IHostingEnvironment hostingEnvironment, ApplicationDbContext context)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.context = context;
        }

        [HttpPost]
        [Route("addProfileImage")]
        public async Task<IActionResult> AddProfileImage(IFormFile file)
        {
            string uploadsRoot = hostingEnvironment.WebRootPath;
            if (file.Length <= 0) return BadRequest("Upload profile picture.");
            string fileName = DateTime.Now.Ticks + file.FileName;
            string filePath = Path.Combine(uploadsRoot, fileName);
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                stream.Dispose();
            }
            GenericActionResult<string> result = new ProfileImagesManager(context).SaveProfileImage(
                new ProfileImage
                    {
                        DateCreated = DateTime.Now,
                        Location = filePath,
                        Name = fileName,
                        IsDeleted = false,
                        UserId = ClaimsPrincipal.Current.Identity.GetUserId()
                    });
            return Ok(new { success = result.Success, message = result.Message });
        }

        [Authorize]
        [HttpPost]
        [Route("updateProfileImage")]
        public async Task<IActionResult> UpdateProfileImage(IFormFile file)
        {
            string uploadsRoot = hostingEnvironment.WebRootPath;
            if (file.Length <= 0) return BadRequest("Upload profile picture.");
            string fileName = DateTime.Now.Ticks + file.FileName;
            string filePath = Path.Combine(uploadsRoot, fileName);
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                stream.Dispose();
            }
            GenericActionResult<string> result = new ProfileImagesManager(context).UpdateProfileImage(
                new ProfileImage
                    {
                        DateCreated = DateTime.Now,
                        Location = filePath,
                        Name = fileName,
                        UserId = ClaimsPrincipal.Current.Identity.GetUserId()
                    });
            return Ok(new { success = result.Success, message = result.Message });
        }

        [HttpGet]
        [Route("getProfileImage/{userId}")]
        public IActionResult GetProfileImage(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return BadRequest("User ID is required");
            GenericActionResult<ProfileImage> result = new ProfileImagesManager(context).GetProfileImageByUserId(userId);
            if (!result.Success) return Ok(new { success = false, message = "Profile image not found" });
            byte[] bytes = System.IO.File.ReadAllBytes(result.Data.Location);
            return Ok(new { success = result.Success, message = result.Message, data = Convert.ToBase64String(bytes) });
        }

        [Authorize]
        [HttpDelete]
        [Route("deleteProfileImage/{imageId}")]
        public IActionResult DeleteProfileImage(int imageId)
        {
            GenericActionResult<string> result = new ProfileImagesManager(context).DeleteProfileImage(imageId);
            return Ok(new { success = result.Success, message = result.Message });
        }

        [HttpGet]
        [Route("getProfileImage")]
        public IActionResult GetProfileImage()
        {
            return GetProfileImage(ClaimsPrincipal.Current.Identity.GetUserId());
        }

        [HttpGet]
        [Route("getAllProfileImages")]
        public IActionResult GetAllProfileImages()
        {
            GenericActionResult<List<ProfileImage>> result = new ProfileImagesManager(context).GetProfileImages();
            List<ImageModel> profileImageModels = result.Data.Select(profileImage => new ImageModel { UserId = profileImage.UserId, Image = Convert.ToBase64String(System.IO.File.ReadAllBytes(profileImage.Location)), ImageName = profileImage.Name, Id = profileImage.Id }).ToList();
            return Ok(new { success = result.Success, message = result.Message, data = profileImageModels });
        }
    }
}