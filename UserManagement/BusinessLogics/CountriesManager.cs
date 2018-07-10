
namespace UserManagement.BusinessLogics
{
    using System.Collections.Generic;
    using System.Linq;

    using UserManagement.Data;

    public class CountriesManager
    {
        private readonly ApplicationDbContext context;

        public CountriesManager(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<Country> GetCountries()
        {
            return context.Countries.ToList();
        }
    }
}
