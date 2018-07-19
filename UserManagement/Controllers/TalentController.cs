
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
        public IActionResult Talents([FromRoute]int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(new { success = true, Data = new TalentsManager(context).GetTalentById(id) });
        }

        [HttpPost]
        public IActionResult AddTalents([FromBody]string name)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(new { success = true, Data = new TalentsManager(context).AddTalent(name) });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTalents([FromRoute]int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(new { success = true, Data = new TalentsManager(context).DeleteTalent(id) });
        }
    }
}