
namespace UserManagement.Controllers
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using UserManagement.BusinessLogics;
    using UserManagement.Data;
    using UserManagement.Models;
    using UserManagement.Models.JobModels;
    using UserManagement.QueryParameters;

    [Produces("application/json")]
    [Route("api/Job")]
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

        [Authorize(Roles = "Organisation")]
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Post(AddJobModel jobModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new JobManager(context, userManager).SaveJob(jobModel, hostingEnvironment.WebRootPath, User.FindFirst(ClaimTypes.NameIdentifier).Value).Result;
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [Authorize(Roles = "Organisation")]
        [HttpPut("{id}"), DisableRequestSizeLimit]
        public IActionResult Put([FromRoute]int id,JobModel jobModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new JobManager(context, userManager).UpdateJob(jobModel, hostingEnvironment.WebRootPath).Result;
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [Authorize(Roles = "Organisation")]
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new JobManager(context, userManager).DeleteJob(id);
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute]int id)
        {
            if (!ModelState.IsValid)return BadRequest(ModelState);
            var result = new JobManager(context, userManager).GetJob(hostingEnvironment.WebRootPath, id);
            if (result.Data == null) return NotFound();
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [HttpGet("all")]
        public IActionResult Get([FromQuery]PaginationParameters paginationParameters)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new JobManager(context,userManager).GetJobs(hostingEnvironment.WebRootPath, paginationParameters.Skip, paginationParameters.Take);
            if (result.Data == null) return NoContent();
            return Ok(new { success = result.Success, message = result.Message, data= result.Data });
        }

        [HttpGet("{userId}")]
        public IActionResult Get([FromRoute]string userId, [FromQuery]PaginationParameters paginationParameters)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new JobManager(context, userManager).GetJobs(hostingEnvironment.WebRootPath, userId, paginationParameters.Skip, paginationParameters.Take);
            if (result.Data == null) return NoContent();
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery]JobSearchParameters jobSearchParameters , [FromQuery]PaginationParameters paginationParameters)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new JobManager(context, userManager).GetJobs(hostingEnvironment.WebRootPath, jobSearchParameters.CountryId, jobSearchParameters.GenderId, jobSearchParameters.TalentId, paginationParameters.Skip, paginationParameters.Take);
            if (result.Data == null) return NoContent();
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

    }
}