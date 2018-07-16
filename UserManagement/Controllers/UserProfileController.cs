
namespace UserManagement.Controllers
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    using UserManagement.BusinessLogics;
    using UserManagement.Data;
    using UserManagement.LocalObjects;
    using UserManagement.Models;
    using UserManagement.Models.UserProfileModels;

    [Produces("application/json")]
    [Route("api/user-details")]
    public class UserProfileController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager;
        public UserProfileController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment, Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.hostingEnvironment = hostingEnvironment;
            this.userManager = userManager;
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("add")]
        public IActionResult AddUserProfileDetails(UserProfileModel userProfileModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Task<GenericActionResult<string>> result = new UserProfileManager(context, userManager).SaveUserDetails(userProfileModel, hostingEnvironment.WebRootPath);
            return Ok(new { success = result.Result.Success, message = result.Result.Message });
        }

        [Authorize]
        [HttpPost, DisableRequestSizeLimit]
        [Route("update")]
        public IActionResult UpdateUserProfileDetails(UserProfileModel userDetailsModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Task<GenericActionResult<string>> result = new UserProfileManager(context, userManager).UpdateUserDetails(userDetailsModel, hostingEnvironment.WebRootPath);
            return Ok(new { success = result.Result.Success, message = result.Result.Message });
        }

        [Authorize]
        [HttpGet]
        [Route("get")]
        public IActionResult GetUserProfileDetails()
        {
            return GetUserProfileDetails(ClaimsPrincipal.Current.Identity.GetUserId());
        }

        [HttpGet]
        [Route("get/all")]
        public IActionResult GetAllUserProfileDetails()
        {
            GenericActionResult<List<UserProfile>> result = new UserProfileManager(context, userManager).GetUserDetails();
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [HttpGet]
        [Route("get/{userId}")]
        public IActionResult GetUserProfileDetails(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return BadRequest("User is required");
            GenericActionResult<UserProfileResponseModel> result = new UserProfileManager(context, userManager).GetUserDetailsByUserId(userId, hostingEnvironment.WebRootPath);
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }
        
       
    }
}