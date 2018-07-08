
namespace UserManagement.Models
{
    using System;
    using System.Linq;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class SeedDatabase
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            ApplicationDbContext context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            context.Database.EnsureCreated();
            if (context.Users.Any()) return;
            ApplicationUser user = new ApplicationUser
                                       {
                                           Email = "a@a.com",
                                           SecurityStamp = Guid.NewGuid().ToString(),
                                           UserName = "admin"
                                       };
            userManager?.CreateAsync(user, "Password@123");
        }
    }
}
