
namespace UserManagement.Controllers
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using UserManagement.BusinessLogics;
    using UserManagement.Data;
    using UserManagement.LocalObjects;
    using UserManagement.Models.JobModels;
    using UserManagement.Models.UserCreditModels;

    [Produces("application/json")]
    [Route("api/job")]
    public class JobController : Controller
    {
        private readonly ApplicationDbContext context;

        public JobController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddJob([FromBody]JobModel jobModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            GenericActionResult<string> result = new JobManager(context).SaveJob(jobModel);
            return Ok(new { success = result.Success, message = result.Message });
        }

        [HttpPost]
        [Route("update")]
        public IActionResult UpdateJob([FromBody]JobModel jobModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            GenericActionResult<string> result = new JobManager(context).UpdateJob(jobModel);
            return Ok(new { success = result.Success, message = result.Message });
        }

        [HttpDelete]
        [Route("delete/{jobId}")]
        public IActionResult DeleteJob(int jobId)
        {
            if(jobId<=0)return BadRequest("Invalid job");
            GenericActionResult<string> result = new JobManager(context).DeleteJob(jobId);
            return Ok(new { success = result.Success, message = result.Message });
        }

        [HttpGet]
        [Route("get/{userId}")]
        public UserCreditModel GetOrganisationTalentsByOrganisation()
        {
            return new UserCreditModel();
        }

        [HttpGet]
        [Route("get/most-popular")]
        public UserCreditModel GetMostPopularJob()
        {
            return new UserCreditModel();
        }

        [HttpGet]
        [Route("search/{countryId}/{genderId}/{talentId}")]
        public UserCreditModel SearchJob(int countryId, int genderId,int talentId )
        {
            return new UserCreditModel();
        }


    }
}