
namespace UserManagement.Controllers
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using UserManagement.BusinessLogics;
    using UserManagement.Data;
    using UserManagement.Models.BusinessTypeModels;

    [Produces("application/json")]
    [Route("api/BusinessType")]
    public class BusinessTypeController : Controller
    {
        private readonly ApplicationDbContext context;

        public BusinessTypeController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<BusinessType> Get()
        {
            return new BusinessTypeManager(context).GetBusinessType();
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var businessTypeModel =new BusinessTypeManager(context).GetBusinessType(id);
            if (businessTypeModel.Data == null)
                return NotFound();
            if (!businessTypeModel.Success) return NoContent();
            return Ok(new { success = businessTypeModel.Success, message = businessTypeModel.Message, data = businessTypeModel.Data });
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] BusinessTypeModel businessTypeModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (id != businessTypeModel.Id)
                return BadRequest();
            var result = new BusinessTypeManager(context).UpdateBusinessType(ObjectConverterManager.ToBusinessType(businessTypeModel));
            return Ok(new { success = result.Result.Success, message = result.Result.Message, data = result.Result.Data });
        }

        [HttpPost]
        public IActionResult Post([FromBody] BusinessTypeModel businessTypeModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = new BusinessTypeManager(context).AddBusinessTypeModel(ObjectConverterManager.ToBusinessType(businessTypeModel));
            return Ok(new { success = result.Result.Success, message = result.Result.Message, data = result.Result.Data });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = new BusinessTypeManager(context).DeleteBusinessTypeModel(id);
            return Ok(new { success = result.Result.Success, message = result.Result.Message, data = result.Result.Data });
        }

    }
}