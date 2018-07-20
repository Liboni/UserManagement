
namespace UserManagement.Controllers
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    using UserManagement.BusinessLogics;
    using UserManagement.Data;
    using UserManagement.Models;
    using UserManagement.Models.JobApplicationModels;

    [Produces("application/json")]
    [Route("api/JobApplication")]
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
        public IActionResult Post([FromBody]AddJobApplicationModel jobApplicationModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new JobApplicationManager(context, userManager).SaveJobApplication(jobApplicationModel);
            new EmailNotification(context, Configuration).SendJobApplicationEmail(result.Data, userManager, hostingEnvironment.WebRootPath);
            return Ok(new { success = result.Success, message = result.Message, data=result.Data });
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromRoute]int id, [FromBody]JobApplicationModel jobApplicationModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id!=jobApplicationModel.Id) return BadRequest();
            var result = new JobApplicationManager(context, userManager).UpdateJobApplication(jobApplicationModel);
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new JobApplicationManager(context, userManager).DeleteJobApplication(id);
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute]int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new JobApplicationManager(context, userManager).GetJobApplications(hostingEnvironment.WebRootPath, id);
            if (result.Data == null) return NotFound();
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [HttpGet("view{id}")]
        public IActionResult View([FromRoute]int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new JobApplicationManager(context, userManager).ViewJobApplication(id);
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [HttpGet("{from}/{count}")]
        public IActionResult Get([FromRoute]int from, [FromRoute]int count)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new JobApplicationManager(context, userManager).GetAllJobApplications(hostingEnvironment.WebRootPath, from, count);
            if (result.Data == null) return NoContent();
            return Ok(new { success = result.Success, message = result.Message, data=result.Data });
        }
      
        [HttpGet("{applicantUserId}/{from}/{count}")]
        public IActionResult GetApplicant([FromRoute]string applicantUserId, [FromRoute]int from, [FromRoute]int count)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new JobApplicationManager(context, userManager).GetJobApplicationByApplicantUserId(applicantUserId, hostingEnvironment.WebRootPath, from, count);
            if (result.Data == null) return NoContent();
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

        [HttpGet("{organisationUserId}/{from}/{count}")]
        public IActionResult GetOrganisation([FromRoute]string organisationUserId, [FromRoute]int from, [FromRoute]int count)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new JobApplicationManager(context, userManager).GetJobApplicationByOrganisationUserId(organisationUserId, hostingEnvironment.WebRootPath, from, count);
            if (result.Data==null) return NoContent();
            return Ok(new { success = result.Success, message = result.Message, data = result.Data });
        }

    }
}