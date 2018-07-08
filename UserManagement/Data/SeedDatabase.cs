
namespace UserManagement.Data
{
    using System;
    using System.Linq;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using UserManagement.Models;

    public class SeedDatabase
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            ApplicationDbContext context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.EnsureCreated();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            if (context.Roles.Any())return;
            await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
            await roleManager.CreateAsync(new IdentityRole { Name = "User" });
            await roleManager.CreateAsync(new IdentityRole { Name = "Organisation" });
            UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            if (context.Users.Any()) return;
            ApplicationUser user = new ApplicationUser
                                       {
                                           Email = "admin@admin.com",
                                           SecurityStamp = Guid.NewGuid().ToString(),
                                           UserName = "admin"
                                       };
            await userManager.CreateAsync(user, "Password@123");
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}
