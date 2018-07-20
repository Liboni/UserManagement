﻿
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
    using UserManagement.Models.OrganisationProfileModels;

    [Authorize]
    [Produces("application/json")]
    [Route("api/OrganisationProfile")]
    public class OrganisationProfileController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager;

        public OrganisationProfileController(ApplicationDbContext context, Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager, IHostingEnvironment hostingEnvironment)
        {
            this.context = context;
            this.userManager = userManager;
            this.hostingEnvironment = hostingEnvironment;
        }

        [Authorize(Roles = "Organisation")]
        [HttpPost]
        public IActionResult Post(AddOrganisationProfileModel organisationDetailsModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new OrganisationProfileManager(context, userManager).SaveOrganisationProfile(organisationDetailsModel, hostingEnvironment.WebRootPath, User.FindFirst(ClaimTypes.NameIdentifier).Value).Result;
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [Authorize(Roles = "Organisation")]
        [HttpPut("{id}")]
        public IActionResult Put([FromRoute]int id,OrganisationProfileModel organisationProfileModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new OrganisationProfileManager(context, userManager).UpdateOrganisationProfile(organisationProfileModel, hostingEnvironment.WebRootPath).Result;
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new OrganisationProfileManager(context, userManager).DeleteOrganisationProfile(id);
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Get(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        [HttpGet("{from}/{count}")]
        public IActionResult Get([FromRoute]int from, [FromRoute]int count)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new OrganisationProfileManager(context, userManager).GetOrganisationProfiles(hostingEnvironment.WebRootPath, from, count);
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [HttpGet("{userId}")]
        public IActionResult Get([FromRoute]string userId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new OrganisationProfileManager(context, userManager).GetOrganisationProfileById(userId, hostingEnvironment.WebRootPath);
            if (result.Data == null) return NotFound();
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });

        }
        
    }
}