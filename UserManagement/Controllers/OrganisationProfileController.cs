
namespace UserManagement.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Security.Claims;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    using UserManagement.BusinessLogics;
    using UserManagement.Data;
    using UserManagement.LocalObjects;
    using UserManagement.Models.OrganisationProfileModels;
    using UserManagement.Models.UserProfileModels;

    [Produces("application/json")]
    [Route("api/OrganisationProfile")]
    public class OrganisationProfileController : Controller
    {
        private readonly ApplicationDbContext context;

        public OrganisationProfileController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        [Route("addOrganisationDetails")]
        public IActionResult AddOrganisationDetails([FromBody]OrganisationProfileModel organisationDetailsModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            GenericActionResult<string> result = new OrganisationProfileManager(context).SaveOrganisationProfile(organisationDetailsModel);
            return Ok(new { success = result.Success, message = result.Message });
        }

        [HttpPost]
        [Route("updateOrganisationDetails")]
        public IActionResult UpdateOrganisationDetails([FromBody]OrganisationProfileModel organisationProfileModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            GenericActionResult<string> result = new OrganisationProfileManager(context).UpdateOrganisationProfile(organisationProfileModel);
            return Ok(new { success = result.Success, message = result.Message });
        }

        [HttpPost]
        [Route("getOrganisationDetails")]
        public IActionResult GetOrganisationDetails()
        {
            return GetOrganisationDetails(ClaimsPrincipal.Current.Identity.GetUserId());
        }

        [HttpPost]
        [Route("getAllOrganisationDetails")]
        public IActionResult GetAllOrganisationDetails()
        {
            GenericActionResult<List<OrganisationProfile>> result = new OrganisationProfileManager(context).GetOrganisationProfiles();
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [HttpPost]
        [Route("getOrganisationDetails/{userId}")]
        public IActionResult GetOrganisationDetails(string userId)
        {
            GenericActionResult<OrganisationProfile> organisationProfileById = new OrganisationProfileManager(context).GetOrganisationProfileById(userId);
            return Ok(new { success = organisationProfileById.Success, message = organisationProfileById.Message, data = organisationProfileById.Data });

        }
        
    }
}