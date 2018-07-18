
namespace UserManagement.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using UserManagement.BusinessLogics;
    using UserManagement.Data;
    using UserManagement.LocalObjects;
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
        public JobApplicationController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IHostingEnvironment hostingEnvironment)
        {
            this.context = context;
            this.userManager = userManager;
            this.hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public IActionResult ApplyJob([FromBody]JobApplicationModel jobApplicationModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            GenericActionResult<string> result = new JobApplicationManager(context, userManager).SaveJobApplication(jobApplicationModel);
            return Ok(new { success = result.Success, message = result.Message });
        }

        [HttpPut("{id}")]
        public IActionResult PutJob([FromRoute]int id, [FromBody]JobApplicationModel jobApplicationModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id!=jobApplicationModel.Id) return BadRequest();
            GenericActionResult<string> result = new JobApplicationManager(context, userManager).UpdateJobApplication(jobApplicationModel);
            return Ok(new { success = result.Success, message = result.Message });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteJobApplication([FromRoute]int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            GenericActionResult<string> result = new JobApplicationManager(context, userManager).DeleteJobApplication(id);
            return Ok(new { success = result.Success, message = result.Message });
        }

        [HttpGet("{from}/{count}")]
        public IActionResult GetAllJobApplication(int from, int count)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = new JobApplicationManager(context, userManager).GetAllJobApplications(hostingEnvironment.WebRootPath, from, count);
            return Ok(new { success = result.Success, message = result.Message, data=result.Data });
        }
       
    }
}