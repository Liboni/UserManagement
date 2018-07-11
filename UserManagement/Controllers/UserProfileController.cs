
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
    using UserManagement.Models.UserProfileModels;

    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UserProfileController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IHostingEnvironment hostingEnvironment;
        public UserProfileController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            this.context = context;
            this.hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        [Route("addProfile")]
        public IActionResult AddUserProfileDetails([FromBody]UserProfileModel userDetailsModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            GenericActionResult<string> result = new UserProfileManager(context).SaveUserDetails(userDetailsModel);
            return Ok(new { success = result.Success, message = result.Message });
        }

        [HttpPost]
        [Route("updateProfile")]
        public IActionResult UpdateUserProfileDetails([FromBody]UserProfileModel userDetailsModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            GenericActionResult<string> result = new UserProfileManager(context).UpdateUserDetails(userDetailsModel);
            return Ok(new { success = result.Success, message = result.Message });
        }

        [Authorize]
        [HttpGet]
        [Route("getProfile")]
        public IActionResult GetUserProfileDetails()
        {
            return GetUserProfileDetails(ClaimsPrincipal.Current.Identity.GetUserId());
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("getAllProfile")]
        public IActionResult GetAllUserProfileDetails()
        {
            GenericActionResult<List<UserProfile>> result = new UserProfileManager(context).GetUserDetails();
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [Authorize]
        [HttpGet]
        [Route("getProfile/{userId}")]
        public IActionResult GetUserProfileDetails(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return BadRequest("User is required");
            GenericActionResult<UserProfile> result = new UserProfileManager(context).GetUserDetailsByUserId(userId);
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [Authorize]
        [HttpPost]
        [Route("addProfileImage")]
        public async Task<IActionResult> AddUserProfileImage(IFormFile file)
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
            return Ok(new { success = result.Success, message = result.Message});
        }

        [Authorize]
        [HttpPost]
        [Route("updateProfileImage")]
        public async Task<IActionResult> UpdateUserProfileImage(IFormFile file)
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
        public IActionResult GetUserProfileImage(string userId)
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
        public IActionResult DeleteUserProfileImage(int imageId)
        {
            GenericActionResult<string> result = new ProfileImagesManager(context).DeleteProfileImage(imageId);
            return Ok(new { success = result.Success, message = result.Message });
        }

        [HttpGet]
        [Route("getProfileImage")]
        public IActionResult GetProfileImage()
        {
            return GetUserProfileImage(ClaimsPrincipal.Current.Identity.GetUserId());
        }

        [HttpGet]
        [Route("getAllProfileImages")]
        public IActionResult GetAllUserProfileImages()
        {
            GenericActionResult<List<ProfileImage>> result= new ProfileImagesManager(context).GetProfileImages();
            List<ProfileImageModel> profileImageModels = result.Data.Select(profileImage => new ProfileImageModel { UserId = profileImage.UserId, Image = Convert.ToBase64String(System.IO.File.ReadAllBytes(profileImage.Location)), ImageName = profileImage.Name, Id = profileImage.Id }).ToList();
            return Ok(new { success = result.Success, message = result.Message, data = profileImageModels });
        }

        [HttpPost]
        [Route("addCredits")]
        public IActionResult AddUserCredits([FromBody]UserCreditModel creditsModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            GenericActionResult<string> result = new UserCreditsManager(context).SaveUserCredit(creditsModel);
            return Ok(new { success = result.Success, message = result.Message});
        }

        [HttpPost]
        [Route("updateCredits")]
        public IActionResult UpdateUserCredits([FromBody]UserCreditModel creditsModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            GenericActionResult<string> result = new UserCreditsManager(context).UpdateUserCredit(creditsModel);
            return Ok(new { success = result.Success, message = result.Message });
        }

        [Authorize]
        [HttpGet]
        [Route("getCredits")]
        public IActionResult GetUserCredits()
        {
           return GetUserCredits(ClaimsPrincipal.Current.Identity.GetUserId());
        }

        [Authorize]
        [HttpDelete]
        [Route("deleteCredits/{creditId}")]
        public IActionResult DeleteUserCredits(int creditId)
        {
            GenericActionResult<string> result = new UserCreditsManager(context).DeleteUserCreditById(creditId);
            return Ok(new { success = result.Success, message = result.Message});
        }

        [HttpGet]
        [Route("getCredits/{userId}")]
        public IActionResult GetUserCredits(string userId)
        {
            GenericActionResult<UserCreditModel> result = new UserCreditsManager(context).GetUserCreditByUserId(userId);
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [HttpGet]
        [Route("getallCredits")]
        public IActionResult GetAllUserCredits()
        {
            GenericActionResult<List<UserCreditModel>> result = new UserCreditsManager(context).GetAllUserCredits();
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }
    }
}