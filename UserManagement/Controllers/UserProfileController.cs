
namespace UserManagement.Controllers
{
    using System.Security.Claims;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    using UserManagement.BusinessLogics;
    using UserManagement.Data;
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
        public IActionResult AddUserProfileDetails(UserProfileModel userProfileModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new UserProfileManager(context, userManager).SaveUserDetails(userProfileModel, hostingEnvironment.WebRootPath).Result;
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [Authorize]
        [HttpPut, DisableRequestSizeLimit]
        public IActionResult UpdateUserProfileDetails([FromRoute]int id,UserProfileModel userDetailsModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id!=userDetailsModel.Id) return BadRequest(ModelState);
            var result = new UserProfileManager(context, userManager).UpdateUserDetails(userDetailsModel, hostingEnvironment.WebRootPath).Result;
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetUserProfileDetails()
        {
            return GetUserProfileDetails(ClaimsPrincipal.Current.Identity.GetUserId());
        }

        [Authorize]
        [HttpGet("{from}/{count}")]
        public IActionResult GetAllUserProfileDetails([FromRoute]int from, [FromRoute]int count)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new UserProfileManager(context, userManager).GetAllUserDetails(hostingEnvironment.WebRootPath, from, count);
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [Authorize]
        [HttpGet("{userId}")]
        public IActionResult GetUserProfileDetails([FromRoute]string userId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (string.IsNullOrEmpty(userId)) return BadRequest("User is required");
            var result = new UserProfileManager(context, userManager).GetUserDetailsByUserId(userId, hostingEnvironment.WebRootPath);
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }
        
       
    } 
}