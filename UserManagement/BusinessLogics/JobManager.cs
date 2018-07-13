
namespace UserManagement.BusinessLogics
{
    using UserManagement.Data;

    public class JobManager
    {
        private readonly ApplicationDbContext context;

        public JobManager(ApplicationDbContext context)
        {
            this.context = context;
        }
    }
}
