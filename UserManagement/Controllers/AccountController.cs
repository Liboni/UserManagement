
namespace UserManagement.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    using UserManagement.BusinessLogics;
    using UserManagement.Data;
    using UserManagement.Models;
    using UserManagement.Models.AccountModels;
    using UserManagement.Providers;
    
    [Produces("application/json")]
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext context;
        private IConfiguration Configuration { get; }

        public AccountController(Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager, ApplicationDbContext context, 
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
            var user = await userManager.FindByNameAsync(loginModel.Email);
            if (!(user != null && await userManager.CheckPasswordAsync(user, loginModel.Password)))
                return Unauthorized();
            if (!await userManager.IsEmailConfirmedAsync(user)) return Ok(new {success=false,message="Check your email to verify your account."});
                return Ok(new { access_token = new ApplicationJwtProvider(Configuration, userManager).JwtTokenBuilder(user).Result });
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]RegisterModel registerModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); 
                var user = new ApplicationUser
                                       {
                                           Email = registerModel.Email,
                                           SecurityStamp = Guid.NewGuid().ToString(),
                                           UserName = registerModel.Email,
                                           PhoneNumber = registerModel.PhoneNumber,
                                           DateCreated = DateTime.Now
                };
            var task = userManager?.CreateAsync(user, registerModel.Password);
            if (task == null) return BadRequest();
            await task;
            if (!task.Result.Succeeded) return Ok(new { success=task.Result.Succeeded, message=task.Result.Errors });
            await userManager.AddToRoleAsync(user, registerModel.RegisterType.ToString());
            new EmailNotification(context, Configuration).SendRegistrationEmail(userManager,user);
            return Ok(new { success= task.Result.Succeeded, message = task.Result.Errors, userId = user.Id});
        }
       
        [HttpGet("verify/{token}")]
        public async Task<IActionResult> VerifyAccount([FromRoute]string token)
        {
            if (!ModelState.IsValid)return BadRequest(ModelState);
            var tokenDetails =new TokenManager(context).GetTokenById(token);
            var result = await userManager.ConfirmEmailAsync(tokenDetails.User, tokenDetails.UserToken);
            return Ok(new { success = result.Succeeded, message = result.Errors });
        }
        
        [HttpGet("forgotPassword/{email}")]
        public async Task<IActionResult> ForgotPassword([FromRoute]string email)
        {
            if (!ModelState.IsValid)return BadRequest(ModelState);
            var user = await userManager.FindByNameAsync(email);
            if (user == null) return BadRequest("User name not registered");
            new EmailNotification(context, Configuration).SendForgotPasswordEmail(userManager, user);
            return Ok(new { success = true, message = "Check your email to reset your password." });
        }

        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordModel resetPasswordModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var tokenDetails = new TokenManager(context).GetTokenById(resetPasswordModel.ResetToken);
            if (tokenDetails == null) return Unauthorized();
            var result = await userManager.ResetPasswordAsync(
                        tokenDetails.User,
                        tokenDetails.UserToken,
                        resetPasswordModel.NewPassword);
            return Ok(new { success = result.Succeeded, message = result.Errors });
        }

        [Authorize]
        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePasswordModel changePasswordModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var user= await userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await userManager.ChangePasswordAsync(
                                        user,
                                        changePasswordModel.OldPassword,
                                        changePasswordModel.NewPassword);
            return Ok(new { success = result.Succeeded, message = result.Errors });
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetRegistered()
        {
            var result = new AccountManager(userManager).GetAllUser();
            if (result.Data == null) return NoContent();
            return Ok(new { success = result.Success, message = result.Message, data=result.Data });
        }
    }
}