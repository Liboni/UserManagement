
namespace UserManagement.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using Newtonsoft.Json;

    using UserManagement.LocalObjects;
    using UserManagement.Models;
    using UserManagement.Models.ValuesModels;

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
                                           UserName = "admin@admin.com",
                                           EmailConfirmed = true
            };
            await userManager.CreateAsync(user, "Password@123");
            await userManager.AddToRoleAsync(user, "Admin");
            HttpWebResponse response = new Http().Get(new HttpRequest<string>
                    {
                        Url = "https://restcountries.eu/rest/v2/all",
                        ContentType = "application/json"
                    });
            using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
            {
                List<CountryModel> countries= JsonConvert.DeserializeObject<List<CountryModel>>(streamReader.ReadToEnd());
                foreach (CountryModel country in countries)
                {
                    context.Countries.Add(new Country { Name = country.Name });
                    context.SaveChanges();
                }
            }
        }
    }
}
