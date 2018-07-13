
namespace UserManagement.Controllers
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNetCore.Authorization;
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
        public UserProfileController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        [Route("addUserProfileDetails")]
        public IActionResult AddUserProfileDetails([FromBody]UserProfileModel userProfileModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            GenericActionResult<string> result = new UserProfileManager(context).SaveUserDetails(userProfileModel);
            return Ok(new { success = result.Success, message = result.Message });
        }

        [HttpPost]
        [Route("updateUserProfileDetails")]
        public IActionResult UpdateUserProfileDetails([FromBody]UserProfileModel userDetailsModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            GenericActionResult<string> result = new UserProfileManager(context).UpdateUserDetails(userDetailsModel);
            return Ok(new { success = result.Success, message = result.Message });
        }

        [Authorize]
        [HttpGet]
        [Route("getUserProfileDetails")]
        public IActionResult GetUserProfileDetails()
        {
            return GetUserProfileDetails(ClaimsPrincipal.Current.Identity.GetUserId());
        }

        [HttpGet]
        [Route("getAllUserProfileDetails")]
        public IActionResult GetAllUserProfileDetails()
        {
            GenericActionResult<List<UserProfile>> result = new UserProfileManager(context).GetUserDetails();
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [Authorize]
        [HttpGet]
        [Route("getUserProfileDetails/{userId}")]
        public IActionResult GetUserProfileDetails(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return BadRequest("User is required");
            GenericActionResult<UserProfile> result = new UserProfileManager(context).GetUserDetailsByUserId(userId);
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }
        
       
    }
}