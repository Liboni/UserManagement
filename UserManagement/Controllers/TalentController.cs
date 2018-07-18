
namespace UserManagement.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using UserManagement.BusinessLogics;
    using UserManagement.Data;

    [Authorize]
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
        public IActionResult Talents()
        {
            return Ok(new { success = true, Data = new TalentsManager(context).GetAllTalents() });
        }

        [HttpGet("{id}")]
        public IActionResult Talents(int id)
        {
            return Ok(new { success = true, Data = new TalentsManager(context).GetTalentById(id) });
        }

        [HttpPost]
        public IActionResult AddTalents([FromBody]string name)
        {
            return Ok(new { success = true, Data = new TalentsManager(context).AddTalent(name) });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTalents(int id)
        {
            return Ok(new { success = true, Data = new TalentsManager(context).DeleteTalent(id) });
        }
    }
}