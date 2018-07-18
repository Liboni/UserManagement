
namespace UserManagement.Controllers
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using UserManagement.BusinessLogics;
    using UserManagement.Data;
    using UserManagement.LocalObjects;
    using UserManagement.Models;
    using UserManagement.Models.JobModels;

    [Authorize]
    [Produces("application/json")]
    [Route("api/job")]
    public class JobController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly UserManager<ApplicationUser> userManager;
        public JobController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.hostingEnvironment = hostingEnvironment;
            this.userManager = userManager;
        }

        [HttpPost]
        public IActionResult AddJob(JobModel jobModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            GenericActionResult<string> result = new JobManager(context, userManager).SaveJob(jobModel, hostingEnvironment.WebRootPath).Result;
            return Ok(new { success = result.Success, message = result.Message });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateJob([FromRoute]int id,JobModel jobModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            GenericActionResult<string> result = new JobManager(context, userManager).UpdateJob(jobModel, hostingEnvironment.WebRootPath).Result;
            return Ok(new { success = result.Success, message = result.Message });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteJob(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            GenericActionResult<string> result = new JobManager(context, userManager).DeleteJob(id);
            return Ok(new { success = result.Success, message = result.Message });
        }

        [HttpGet("{id}")]
        public IActionResult GetOrganisationTalents(int id)
        {
            GenericActionResult<JobResponseModel> result = new JobManager(context, userManager).GetJob(hostingEnvironment.WebRootPath, id);
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [HttpGet("{from}/{count}")]
        public IActionResult GetOrganisationTalents(int from, int count)
        {
            GenericActionResult<List<JobResponseModel>> result = new JobManager(context,userManager).GetJobs(hostingEnvironment.WebRootPath,from, count);
            return Ok(new { success = result.Success, message = result.Message, data= result.Data });
        }

        [HttpGet("{userId}/{from}/{count}")]
        public IActionResult GetOrganisationTalents(string userId,int from, int count)
        {
            GenericActionResult<List<JobResponseModel>> result = new JobManager(context, userManager).GetJobs(hostingEnvironment.WebRootPath, userId, from, count);
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [HttpGet("{countryId}/{genderId}/{talentId}/{from}/{count}")]
        public IActionResult SearchJob(int countryId, int genderId,int talentId, int from,int count )
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            GenericActionResult<List<JobResponseModel>> result = new JobManager(context, userManager).GetJobs(hostingEnvironment.WebRootPath,countryId,genderId,talentId, from, count);
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

    }
}