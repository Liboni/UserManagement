
namespace UserManagement.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using UserManagement.Data;
    using UserManagement.Models.JobApplicationModels;

    [Produces("application/json")]
    [Route("api/job-application")]
    public class JobApplicationController : Controller
    {
        private readonly ApplicationDbContext context;

        public JobApplicationController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        [Route("apply")]
        public IActionResult ApplyJob([FromBody]JobApplicationModel jobModel)
        {
          return Ok();
        }

        [HttpPost]
        [Route("get/userId")]
        public IActionResult GetApplicationByUserId([FromBody]JobApplicationModel jobModel)
        {
            return Ok();
        }

        [HttpPost]
        [Route("remove/userId")]
        public IActionResult DeleteApplicationByUserId([FromBody]JobApplicationModel jobModel)
        {
            return Ok();
        }
    }
}