
namespace UserManagement.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using UserManagement.BusinessLogics;
    using UserManagement.Data;

    [Produces("application/json")]
    [Route("api/Talent")]
    public class TalentController : Controller
    {
        private readonly ApplicationDbContext context;

        public TalentController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("talent/all")]
        public IActionResult Talents()
        {
            return Ok(new { success = true, Data = new TalentsManager(context).GetAllTalents() });
        }

        [HttpGet]
        [Route("talent/{id}")]
        public IActionResult Talents(int id)
        {
            return Ok(new { success = true, Data = new TalentsManager(context).GetTalentById(id) });
        }

        [HttpGet]
        [Route("talent/add/{name}")]
        public IActionResult AddTalents(string name)
        {
            return Ok(new { success = true, Data = new TalentsManager(context).AddTalent(name) });
        }

        [HttpGet]
        [Route("talent/remove/{id}")]
        public IActionResult DeleteTalents(int id)
        {
            return Ok(new { success = true, Data = new TalentsManager(context).DeleteTalent(id) });
        }
    }
}