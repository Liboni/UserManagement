
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

        [HttpPost]
        public IActionResult Post(OrganisationProfileModel organisationDetailsModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new OrganisationProfileManager(context, userManager).SaveOrganisationProfile(organisationDetailsModel, hostingEnvironment.WebRootPath).Result;
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromRoute]int id,OrganisationProfileModel organisationProfileModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new OrganisationProfileManager(context, userManager).UpdateOrganisationProfile(organisationProfileModel, hostingEnvironment.WebRootPath).Result;
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetOrganisationDetails()
        {
            return GetOrganisationDetails(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        [HttpGet("{from}/{count}")]
        public IActionResult GetAllOrganisationDetails([FromRoute]int from, [FromRoute]int count)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new OrganisationProfileManager(context, userManager).GetOrganisationProfiles(hostingEnvironment.WebRootPath, from, count);
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [HttpGet("{userId}")]
        public IActionResult GetOrganisationDetails([FromRoute]string userId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new OrganisationProfileManager(context, userManager).GetOrganisationProfileById(userId, hostingEnvironment.WebRootPath);
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });

        }
        
    }
}