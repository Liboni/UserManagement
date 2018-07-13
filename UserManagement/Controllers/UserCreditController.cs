﻿
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
        [Route("add")]
        public IActionResult AddUserCredits([FromBody]UserCreditModel creditsModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            GenericActionResult<string> result = new UserCreditsManager(context).SaveUserCredit(creditsModel);
            return Ok(new { success = result.Success, message = result.Message });
        }

        [HttpPost]
        [Route("update")]
        public IActionResult UpdateUserCredits([FromBody]UserCreditModel creditsModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            GenericActionResult<string> result = new UserCreditsManager(context).UpdateUserCredit(creditsModel);
            return Ok(new { success = result.Success, message = result.Message });
        }

        [Authorize]
        [HttpGet]
        [Route("get")]
        public IActionResult GetUserCredits()
        {
            return GetUserCredits(ClaimsPrincipal.Current.Identity.GetUserId());
        }

        [Authorize]
        [HttpDelete]
        [Route("delete/{creditId}")]
        public IActionResult DeleteUserCredits(int creditId)
        {
            GenericActionResult<string> result = new UserCreditsManager(context).DeleteUserCreditById(creditId);
            return Ok(new { success = result.Success, message = result.Message });
        }

        [HttpGet]
        [Route("get/{userId}")]
        public IActionResult GetUserCredits(string userId)
        {
            GenericActionResult<UserCreditModel> result = new UserCreditsManager(context).GetUserCreditByUserId(userId);
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [HttpGet]
        [Route("get/all")]
        public IActionResult GetAllUserCredits()
        {
            GenericActionResult<List<UserCreditModel>> result = new UserCreditsManager(context).GetAllUserCredits();
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }
    }
}