
namespace UserManagement.Controllers
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using UserManagement.Models.UserCreditModels;

    [Produces("application/json")]
    [Route("api/Job")]
    public class JobController : Controller
    {
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