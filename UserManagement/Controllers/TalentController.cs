
namespace UserManagement.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using UserManagement.BusinessLogics;
    using UserManagement.Data;
    using UserManagement.Models.TalentModels;

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
            var talents = new TalentsManager(context).GetAllTalents();
            if (talents == null) return NoContent();
            return Ok(new { success = true,message="", Data = talents });
        }

        [HttpGet("{id}")]
        public IActionResult Talents([FromRoute]int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var talent = new TalentsManager(context).GetTalentById(id);
            if (talent == null) return NotFound();
            return Ok(new { success = true,message="", data = talent });
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromRoute]int id, TalentModel talent)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id!=talent.Id) return BadRequest(ModelState);
            var updateTalent = new TalentsManager(context).UpdateTalent(talent);
            return Ok(new { success = updateTalent.Result.Success,message = updateTalent.Result.Message, data = updateTalent.Result.Data });
        }

        [HttpPost]
        public IActionResult Post([FromBody]TalentModel talentModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var talent = new TalentsManager(context).AddTalent(talentModel.Name);
            return Ok(new { success = talent.Success,message=talent.Message, data = talent.Data });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var talent = new TalentsManager(context).DeleteTalent(id);
            return Ok(new { success = talent.Success,message= talent.Message, data = talent.Data });
        }
    }
}