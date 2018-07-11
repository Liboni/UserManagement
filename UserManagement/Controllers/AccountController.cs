
namespace UserManagement.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    using UserManagement.BusinessLogics;
    using UserManagement.Data;
    using UserManagement.Models;
    using UserManagement.Models.AccountModels;
    using UserManagement.Providers;

    using IdentityResult = Microsoft.AspNetCore.Identity.IdentityResult;

    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext context;
        private IConfiguration Configuration { get; }

        public AccountController(
            Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager, ApplicationDbContext context, 
            IConfiguration configuration) 
        {
            this.userManager = userManager;
            this.context = context;
            Configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]LoginModel loginModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            ApplicationUser user = await userManager.FindByNameAsync(loginModel.Email);
            if (user != null && await userManager.CheckPasswordAsync(user, loginModel.Password) && await userManager.IsEmailConfirmedAsync(user))
            {
                return Ok(new { access_token = new ApplicationJwtProvider(Configuration, userManager).JwtTokenBuilder(user).Result });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]RegisterModel registerModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); 
                ApplicationUser user = new ApplicationUser
                                       {
                                           Email = registerModel.Email,
                                           SecurityStamp = Guid.NewGuid().ToString(),
                                           UserName = registerModel.Email
              };
            Task<IdentityResult> task = userManager?.CreateAsync(user, registerModel.Password);
            if (task == null) return BadRequest();
            await task;
            if (!task.Result.Succeeded) return Ok(new { success=task.Result.Succeeded, message=task.Result.Errors });
            await userManager.AddToRoleAsync(user, registerModel.RegisterType.ToString());
            new EmailNotification(context, Configuration).SendRegistrationEmail(userManager,user);
            return Ok(new { success= task.Result.Succeeded, message = task.Result.Errors, userId = user.Id});
        }
       
        [HttpGet]
        [Route("verify/{token}")]
        public async Task<IActionResult> VerifyAccount(string token)
        {
            if (String.IsNullOrEmpty(token)) return BadRequest();
            Token tokenDetails =new TokenManager(context).GetTokenById(token);
            IdentityResult result = await userManager.ConfirmEmailAsync(tokenDetails.User, tokenDetails.UserToken);
            return Ok(new { success = result.Succeeded, message = result.Errors });
        }
        
        [HttpGet]
        [Route("forgotPassword/{email}")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (String.IsNullOrEmpty(email)) return BadRequest("Email address is required");
            ApplicationUser user = await userManager.FindByNameAsync(email);
            if (user == null) return BadRequest("User name not registered");
            new EmailNotification(context, Configuration).SendForgotPasswordEmail(userManager, user);
            return Ok(new { success = true, message = "Check your email to reset your password." });
        }

        [HttpPost]
        [Route("resetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordModel resetPasswordModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Token tokenDetails = new TokenManager(context).GetTokenById(resetPasswordModel.ResetToken);
            IdentityResult result = await userManager.ResetPasswordAsync(
                        tokenDetails.User,
                        tokenDetails.UserToken,
                        resetPasswordModel.NewPassword);
            return Ok(new { success = result.Succeeded, message = result.Errors });
        }

        [Authorize]
        [HttpGet]
        [Route("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePasswordModel changePasswordModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            ApplicationUser user= await userManager.FindByIdAsync(ClaimsPrincipal.Current.Identity.GetUserId());
            IdentityResult result = await userManager.ChangePasswordAsync(
                                        user,
                                        changePasswordModel.OldPassword,
                                        changePasswordModel.NewPassword);
            return Ok(new { success = result.Succeeded, message = result.Errors });
        }
    }
}