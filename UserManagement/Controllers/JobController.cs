
namespace UserManagement.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using UserManagement.BusinessLogics;
    using UserManagement.Data;
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
            var result = new JobManager(context, userManager).SaveJob(jobModel, hostingEnvironment.WebRootPath).Result;
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateJob([FromRoute]int id,JobModel jobModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new JobManager(context, userManager).UpdateJob(jobModel, hostingEnvironment.WebRootPath).Result;
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteJob([FromRoute]int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new JobManager(context, userManager).DeleteJob(id);
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [HttpGet("{id}")]
        public IActionResult GetOrganisationTalents([FromRoute]int id)
        {
            if (!ModelState.IsValid)return BadRequest(ModelState);
            var result = new JobManager(context, userManager).GetJob(hostingEnvironment.WebRootPath, id);
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [HttpGet("{from}/{count}")]
        public IActionResult GetOrganisationTalents([FromRoute]int from, [FromRoute]int count)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new JobManager(context,userManager).GetJobs(hostingEnvironment.WebRootPath,from, count);
            return Ok(new { success = result.Success, message = result.Message, data= result.Data });
        }

        [HttpGet("{userId}/{from}/{count}")]
        public IActionResult GetOrganisationTalents([FromRoute]string userId,[FromRoute]int from, [FromRoute]int count)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new JobManager(context, userManager).GetJobs(hostingEnvironment.WebRootPath, userId, from, count);
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [HttpGet("{countryId}/{genderId}/{talentId}/{from}/{count}")]
        public IActionResult SearchJob([FromRoute]int countryId, [FromRoute]int genderId,[FromRoute]int talentId,[FromRoute] int from,[FromRoute]int count )
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new JobManager(context, userManager).GetJobs(hostingEnvironment.WebRootPath,countryId,genderId,talentId, from, count);
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

    }
}