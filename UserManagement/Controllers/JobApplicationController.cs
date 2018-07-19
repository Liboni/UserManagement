
namespace UserManagement.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    using UserManagement.BusinessLogics;
    using UserManagement.Data;
    using UserManagement.Models;
    using UserManagement.Models.JobApplicationModels;

    [Authorize]
    [Produces("application/json")]
    [Route("api/apply")]
    public class JobApplicationController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly UserManager<ApplicationUser> userManager;
        private IConfiguration Configuration { get; }
        public JobApplicationController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            this.context = context;
            this.userManager = userManager;
            this.hostingEnvironment = hostingEnvironment;
            Configuration = configuration;
        }

        [HttpPost]
        public IActionResult ApplyJob([FromBody]JobApplicationModel jobApplicationModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new JobApplicationManager(context, userManager).SaveJobApplication(jobApplicationModel);
            new EmailNotification(context, Configuration).SendJobApplicationEmail(result.Data, userManager, hostingEnvironment.WebRootPath);
            return Ok(new { success = result.Success, message = result.Message, data=result.Data });
        }

        [HttpPut("{id}")]
        public IActionResult PutJob([FromRoute]int id, [FromBody]JobApplicationModel jobApplicationModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id!=jobApplicationModel.Id) return BadRequest();
            var result = new JobApplicationManager(context, userManager).UpdateJobApplication(jobApplicationModel);
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteJobApplication([FromRoute]int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new JobApplicationManager(context, userManager).DeleteJobApplication(id);
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [HttpGet("{id}")]
        public IActionResult GetJobApplication([FromRoute]int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new JobApplicationManager(context, userManager).GetJobApplications(hostingEnvironment.WebRootPath, id);
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [HttpGet("view{id}")]
        public IActionResult ViewJobApplication([FromRoute]int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new JobApplicationManager(context, userManager).ViewJobApplication(id);
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [HttpGet("{from}/{count}")]
        public IActionResult GetAllJobApplication([FromRoute]int from, [FromRoute]int count)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new JobApplicationManager(context, userManager).GetAllJobApplications(hostingEnvironment.WebRootPath, from, count);
            return Ok(new { success = result.Success, message = result.Message, data=result.Data });
        }
      
        [HttpGet("{organisationUserId}/{from}/{count}")]
        public IActionResult GetAllJobApplication([FromRoute]string organisationUserId, [FromRoute]int from, [FromRoute]int count)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new JobApplicationManager(context, userManager).GetJobApplicationByUserId(organisationUserId,hostingEnvironment.WebRootPath, from, count);
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

    }
}