
namespace UserManagement.Controllers
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    using UserManagement.BusinessLogics;
    using UserManagement.Data;
    using UserManagement.LocalObjects;
    using UserManagement.Models;
    using UserManagement.Models.OrganisationProfileModels;

    [Produces("application/json")]
    [Route("api/organisation-details")]
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
        public IActionResult AddOrganisationDetails(OrganisationProfileModel organisationDetailsModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Task<GenericActionResult<string>> result = new OrganisationProfileManager(context, userManager).SaveOrganisationProfile(organisationDetailsModel, hostingEnvironment.WebRootPath);
            return Ok(new { success = result.Result.Success, message = result.Result.Message });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrganisationDetails([FromRoute]int id,OrganisationProfileModel organisationProfileModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Task<GenericActionResult<string>> result = new OrganisationProfileManager(context, userManager).UpdateOrganisationProfile(organisationProfileModel, hostingEnvironment.WebRootPath);
            return Ok(new { success = result.Result.Success, message = result.Result.Message });
        }

        [HttpGet]
        public IActionResult GetOrganisationDetails()
        {
            return GetOrganisationDetails(ClaimsPrincipal.Current.Identity.GetUserId());
        }

        [HttpGet("{from}/{count}")]
        public IActionResult GetAllOrganisationDetails(int from, int count)
        {
            GenericActionResult<List<OrganisationProfileResponseModel>> result = new OrganisationProfileManager(context, userManager).GetOrganisationProfiles(hostingEnvironment.WebRootPath, from, count);
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [HttpGet("{userId}")]
        public IActionResult GetOrganisationDetails(string userId)
        {
            var result = new OrganisationProfileManager(context, userManager).GetOrganisationProfileById(userId, hostingEnvironment.WebRootPath);
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });

        }
        
    }
}