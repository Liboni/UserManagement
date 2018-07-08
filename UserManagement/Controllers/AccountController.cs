
namespace UserManagement.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using UserManagement.Models;
    using UserManagement.Providers;

    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        public AccountController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]LoginModel loginModel)
        {
            ApplicationUser user = await userManager.FindByNameAsync(loginModel.Email);
            if (user != null && await userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                return Ok(new { access_token = new ApplicationJwtProvider().JwtTokenBuilder(user) });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("registerUser")]
        public async Task<IdentityResult> RegisterUser([FromBody]RegisterModel registerModel)
        {
              ApplicationUser user = new ApplicationUser
                                       {
                                           Email = registerModel.Email,
                                           SecurityStamp = Guid.NewGuid().ToString(),
                                           UserName = registerModel.Email
              };
            Task<IdentityResult> task = userManager?.CreateAsync(user, registerModel.Password);
            if (task == null) return IdentityResult.Failed();
            await task;
            return task.Result;
        }

        [HttpPost]
        [Route("registerOrganisation")]
        public async Task<IdentityResult> RegisterOrganisation([FromBody]RegisterModel registerModel)
        {
            ApplicationUser user = new ApplicationUser
                                       {
                                           Email = registerModel.Email,
                                           SecurityStamp = Guid.NewGuid().ToString(),
                                           UserName = registerModel.Email
                                       };
            Task<IdentityResult> task = userManager?.CreateAsync(user, registerModel.Password);
            if (task == null) return IdentityResult.Failed();
            await task;
            return task.Result;
        }

        [HttpGet]
        [Route("verify/{token}")]
        public  IdentityResult VerifyEmailAddress(string token)
        {
           return IdentityResult.Success;
        }

        [HttpGet]
        [Route("forgotPassword/{email}")]
        public IdentityResult ForgotPassword(string email)
        {
            return IdentityResult.Success;
        }

        [HttpPost]
        [Route("resetPassword")]
        public IdentityResult ResetPassword([FromBody]ResetPasswordModel resetPasswordModel)
        {
            return IdentityResult.Success;
        }

        [HttpGet]
        [Route("changePassword")]
        public IdentityResult ChangePassword([FromBody]ChangePasswordModel changePasswordModel)
        {
            return IdentityResult.Success;
        }
    }
}