
namespace UserManagement.Controllers
{
    using System.Security.Claims;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNetCore.Mvc;

    using UserManagement.BusinessLogics;
    using UserManagement.Data;
    using UserManagement.Models;
    using UserManagement.Models.UserCreditModels;

    [Produces("application/json")]
    [Route("api/credit")]
    public class UserCreditController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager;
        public UserCreditController(ApplicationDbContext context, Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpPost]
        public IActionResult AddUserCredits([FromBody]UserCreditModel creditsModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new UserCreditsManager(context, userManager).SaveUserCredit(creditsModel);
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUserCredits([FromRoute]int id,[FromBody]UserCreditModel creditsModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id!=creditsModel.Id) return BadRequest();
            var result = new UserCreditsManager(context, userManager).UpdateUserCredit(creditsModel);
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }
        
        [HttpGet("user")]
        public IActionResult GetUserCredits()
        {
            return GetUserCredits(ClaimsPrincipal.Current.Identity.GetUserId());
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteUserCredits([FromRoute]int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new UserCreditsManager(context, userManager).DeleteUserCreditById(id);
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [HttpGet("{userId}")]
        public IActionResult GetUserCredits([FromRoute]string userId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new UserCreditsManager(context, userManager).GetUserCreditByUserId(userId);
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [HttpGet]
        public IActionResult GetAllUserCredits()
        {
            var result = new UserCreditsManager(context, userManager).GetAllUserCredits();
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }
    }
}