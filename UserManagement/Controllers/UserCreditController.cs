
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
    using UserManagement.Models.UserCreditModels;

    [Produces("application/json")]
    [Route("api/credit")]
    public class UserCreditController : Controller
    {
        private readonly ApplicationDbContext context;

        public UserCreditController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public IActionResult AddUserCredits([FromBody]UserCreditModel creditsModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            GenericActionResult<string> result = new UserCreditsManager(context).SaveUserCredit(creditsModel);
            return Ok(new { success = result.Success, message = result.Message });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUserCredits([FromRoute]int id,[FromBody]UserCreditModel creditsModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id!=creditsModel.Id) return BadRequest();
            GenericActionResult<string> result = new UserCreditsManager(context).UpdateUserCredit(creditsModel);
            return Ok(new { success = result.Success, message = result.Message });
        }
        
        [HttpGet]
        public IActionResult GetUserCredits()
        {
            return GetUserCredits(ClaimsPrincipal.Current.Identity.GetUserId());
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteUserCredits(int id)
        {
            GenericActionResult<string> result = new UserCreditsManager(context).DeleteUserCreditById(id);
            return Ok(new { success = result.Success, message = result.Message });
        }

        [HttpGet("{userId}")]
        public IActionResult GetUserCredits(string userId)
        {
            GenericActionResult<UserCreditModel> result = new UserCreditsManager(context).GetUserCreditByUserId(userId);
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [HttpGet]
        public IActionResult GetAllUserCredits()
        {
            GenericActionResult<List<UserCreditModel>> result = new UserCreditsManager(context).GetAllUserCredits();
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }
    }
}