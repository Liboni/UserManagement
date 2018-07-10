
namespace UserManagement.Controllers
{
    using System.Collections.Generic;
    using System.IO;

    using Microsoft.AspNetCore.Mvc;

    using UserManagement.Models;

    [Produces("application/json")]
    [Route("api/OrganisationProfile")]
    public class OrganisationProfileController : Controller
    {
        [HttpPost]
        [Route("addOrganisationDetails")]
        public IActionResult AddOrganisationDetails([FromBody]OrganisationDetailsModel organisationDetailsModel)
        {
            return Accepted();
        }

        [HttpPost]
        [Route("getOrganisationDetails")]
        public OrganisationDetailsModel GetOrganisationDetails()
        {
            return new OrganisationDetailsModel();
        }

        [HttpPost]
        [Route("getAllOrganisationDetails")]
        public List<OrganisationDetailsModel> GetAllOrganisationDetails()
        {
            return new List<OrganisationDetailsModel>();
        }

        [HttpPost]
        [Route("getOrganisationDetails/{userId}")]
        public OrganisationDetailsModel GetUserDetails(string userId)
        {
            return new OrganisationDetailsModel();
        }

        [HttpPost]
        [Route("uploadOrganisationProfile")]
        public IActionResult UploadProfile()
        {
            return Accepted();
        }

        [HttpPost]
        [Route("getOrganisationProfile/{userId}")]
        public IActionResult UploadProfile(string userId)
        {
            return Accepted();
        }

        [HttpPost]
        [Route("getOrganisationProfile")]
        public Stream GetProfile()
        {
            return Stream.Synchronized(Stream.Null);
        }

        [HttpPost]
        [Route("getAllOrganisationProfile")]
        public Stream GetAllProfile()
        {
            return Stream.Synchronized(Stream.Null);
        }

        [HttpPost]
        [Route("addOrganisationTalents")]
        public IActionResult AddCredits([FromBody]UserCreditModel creditsModel)
        {
            return Accepted();
        }

        [HttpPost]
        [Route("getOrganisationTalents")]   
        public UserCreditModel GetOrganisationTalents()
        {
            return new UserCreditModel();
        }

        [HttpPost]
        [Route("getOrganisationTalents/{userId}")]
        public UserCreditModel GetOrganisationTalents(string userId)
        {
            return new UserCreditModel();
        }

        [HttpPost]
        [Route("getAllOrganisationTalents")]
        public List<UserCreditModel> GetAllOrganisationTalents()
        {
            return new List<UserCreditModel>();
        }
    }
}