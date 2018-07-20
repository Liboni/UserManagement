
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
        [HttpPost]
        public IActionResult Post(AddJobModel jobModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new JobManager(context, userManager).SaveJob(jobModel, hostingEnvironment.WebRootPath, User.FindFirst(ClaimTypes.NameIdentifier).Value).Result;
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [Authorize(Roles = "Organisation")]
        [HttpPut("{id}")]
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

        [HttpGet("{from}/{count}")]
        public IActionResult Get([FromRoute]int from, [FromRoute]int count)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new JobManager(context,userManager).GetJobs(hostingEnvironment.WebRootPath,from, count);
            if (result.Data == null) return NoContent();
            return Ok(new { success = result.Success, message = result.Message, data= result.Data });
        }

        [HttpGet("{userId}/{from}/{count}")]
        public IActionResult Get([FromRoute]string userId,[FromRoute]int from, [FromRoute]int count)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new JobManager(context, userManager).GetJobs(hostingEnvironment.WebRootPath, userId, from, count);
            if (result.Data == null) return NoContent();
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [HttpGet("{countryId}/{genderId}/{talentId}/{from}/{count}")]
        public IActionResult Search([FromRoute]int countryId, [FromRoute]int genderId,[FromRoute]int talentId,[FromRoute] int from,[FromRoute]int count )
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new JobManager(context, userManager).GetJobs(hostingEnvironment.WebRootPath,countryId,genderId,talentId, from, count);
            if (result.Data == null) return NoContent();
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

    }
}