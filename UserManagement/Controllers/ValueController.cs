
namespace UserManagement.Controllers
{
    using System;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    using UserManagement.BusinessLogics;
    using UserManagement.Data;
    using UserManagement.Enums;
    using UserManagement.Models.ValuesModels;

    [Produces("application/json")]
    [Route("api/value")]
    public class ValueController : Controller
    {
        private readonly ApplicationDbContext context;

        public ValueController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("country/all")]
        public IActionResult Country()
        {
           return Ok(new {success = true,Data= new CountriesManager(context).GetCountries() });
        }
        
        [HttpGet]
        [Route("gender/all")]
        public IActionResult Gender()
        {
            Array values = Enum.GetValues(typeof(Gender));
            return Ok(new { success = true, Data = (from object value in values select new GenderModel { Id = Convert.ToByte(value), Name = Enum.GetName(typeof(Gender), value) }).ToList() });
        }
    }
}