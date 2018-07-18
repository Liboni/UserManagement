
namespace UserManagement.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using UserManagement.BusinessLogics;
    using UserManagement.Data;
    using UserManagement.LocalObjects;
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
        public IEnumerable<BusinessType> GetBusinessTypeModel()
        {
            return new BusinessTypeManager(context).GetBusinessType();
        }

        [HttpGet("{id}")]
        public IActionResult GetBusinessTypeModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            Task<GenericActionResult<BusinessType>> businessTypeModel =new BusinessTypeManager(context).GetBusinessType(id);
            if (businessTypeModel.Result.Data == null)
                return NotFound();
            if (!businessTypeModel.Result.Success) return NoContent();
            return Ok(new { success = businessTypeModel.Result.Success, message = businessTypeModel.Result.Message, data = businessTypeModel.Result.Data });
        }

        [HttpPut("{id}")]
        public IActionResult PutBusinessTypeModel([FromRoute] int id, [FromBody] BusinessTypeModel businessTypeModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (id != businessTypeModel.Id)
                return BadRequest();
            Task<GenericActionResult<BusinessType>> result = new BusinessTypeManager(context).UpdateBusinessType(ObjectConverter.ToBusinessType(businessTypeModel));
            return Ok(new { success = result.Result.Success, message = result.Result.Message, data = result.Result.Data });
        }

        [HttpPost]
        public IActionResult PostBusinessTypeModel([FromBody] BusinessTypeModel businessTypeModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
           Task<GenericActionResult<BusinessType>> result = new BusinessTypeManager(context).AddBusinessTypeModel(ObjectConverter.ToBusinessType(businessTypeModel));
            return Ok(new { success = result.Result.Success, message = result.Result.Message, data = result.Result.Data });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBusinessTypeModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
           var result = new BusinessTypeManager(context).DeleteBusinessTypeModel(id);
            return Ok(new { success = result.Result.Success, message = result.Result.Message, data = result.Result.Data });
        }

    }
}