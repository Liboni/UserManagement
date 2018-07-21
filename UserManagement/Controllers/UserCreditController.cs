
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
    using UserManagement.QueryParameters;

    [Authorize]
    [Produces("application/json")]
    [Route("api/UserCredit")]
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
        public IActionResult Post([FromBody]AddUserCreditModel addUserCreditModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var creditsModel = new UserCreditModel{UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value ,Production = addUserCreditModel.Production,TalentId = addUserCreditModel.TalentId};
            var result = new UserCreditsManager(context, userManager).SaveUserCredit(creditsModel);
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [Authorize(Roles = "User")]
        [HttpPut("{id}")]
        public IActionResult Put([FromRoute]int id,[FromBody]UserCreditModel creditsModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id!=creditsModel.Id) return BadRequest();
            var result = new UserCreditsManager(context, userManager).UpdateUserCredit(creditsModel);
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            return Get(ClaimsPrincipal.Current.Identity.GetUserId());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new UserCreditsManager(context, userManager).GetUserCreditById(id);
            if (result.Data == null) return NotFound();
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [Authorize(Roles = "User")]
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new UserCreditsManager(context, userManager).DeleteUserCreditById(id);
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [HttpGet("{userId}")]
        public IActionResult Get([FromRoute]string userId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new UserCreditsManager(context, userManager).GetUserCreditByUserId(userId);
            if (result.Data == null) return NotFound();
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [HttpGet("all")]
        public IActionResult Get([FromQuery]PaginationParameters paginationParameters)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new UserCreditsManager(context, userManager).GetAllUserCredits(paginationParameters.Skip, paginationParameters.Take);
            if (result.Data == null) return NoContent();
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }
    }
}