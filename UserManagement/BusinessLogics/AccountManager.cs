
namespace UserManagement.BusinessLogics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using UserManagement.LocalObjects;
    using UserManagement.Models;
    using UserManagement.Models.AccountModels;

    public class AccountManager
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AccountManager(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public GenericActionResult<List<UserModel>> GetAllUser()
        {
            try
            {
                var users = userManager.Users.ToListAsync().Result;
                return GenericActionResult<List<UserModel>>.FromObject(users.Select(a => new UserModel
                {
                    UserId = a.Id,
                    DateCreated = a.DateCreated,
                    Email = a.Email,
                    UserName = a.UserName,
                    PhoneNumber = a.PhoneNumber,
                    IsConfirmed = a.EmailConfirmed,
                    Roles = userManager.GetRolesAsync(a).Result
                }).ToList());
            }
            catch (Exception exception)
            {
                return new GenericActionResult<List<UserModel>>(exception.Message);
            }
        }
    }
}
