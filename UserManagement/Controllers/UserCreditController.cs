
namespace UserManagement.Controllers
{
    using System.Security.Claims;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using UserManagement.BusinessLogics;
    using UserManagement.Data;
    using UserManagement.Models;
    using UserManagement.Models.UserCreditModels;

    [Authorize]
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

        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult AddUserCredits([FromBody]AddUserCreditModel addUserCreditModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var creditsModel = new UserCreditModel{UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value ,Production = addUserCreditModel.Production,TalentId = addUserCreditModel.TalentId};
            var result = new UserCreditsManager(context, userManager).SaveUserCredit(creditsModel);
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [Authorize(Roles = "User")]
        [HttpPut("{id}")]
        public IActionResult UpdateUserCredits([FromRoute]int id,[FromBody]UserCreditModel creditsModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id!=creditsModel.Id) return BadRequest();
            var result = new UserCreditsManager(context, userManager).UpdateUserCredit(creditsModel);
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }
        
        [HttpGet]
        public IActionResult GetUserCredits()
        {
            return GetUserCredits(ClaimsPrincipal.Current.Identity.GetUserId());
        }

        [Authorize(Roles = "User")]
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
            if (result.Data == null) return NotFound();
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [HttpGet("all")]
        public IActionResult GetAllUserCredits()
        {
            var result = new UserCreditsManager(context, userManager).GetAllUserCredits();
            if (result.Data == null) return NoContent();
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }
    }
}