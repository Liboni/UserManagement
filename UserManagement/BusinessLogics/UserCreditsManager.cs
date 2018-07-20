
namespace UserManagement.BusinessLogics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Identity;

    using UserManagement.Data;
    using UserManagement.LocalObjects;
    using UserManagement.Models;
    using UserManagement.Models.UserCreditModels;

    public class UserCreditsManager
    {
        private readonly ApplicationDbContext context;

        private readonly UserManager<ApplicationUser> userManager;
        public UserCreditsManager(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public GenericActionResult<UserCredit> SaveUserCredit(UserCreditModel userCreditModel)
        {
            try
            {
                var userCredit = new UserCredit
                            {
                                Production = userCreditModel.Production,
                                TalentId = userCreditModel.TalentId,
                                UserId = userCreditModel.UserId,
                                IsDeleted = false,
                                DateCreated = DateTime.Now
                            };
                context.UserCredits.Add(userCredit);
                context.SaveChanges();
                return new GenericActionResult<UserCredit>(true, "User credit saved successfully.", userCredit);
            }
            catch (Exception) { return new GenericActionResult<UserCredit>("Failed to save credit, please try again or contact the administrator."); }
        }

        public GenericActionResult<UserCredit> UpdateUserCredit(UserCreditModel userCreditModel)
        {
            try
            {
                UserCredit  credit = context.UserCredits.Find(userCreditModel.Id);
                credit.Production = userCreditModel.Production;
                credit.TalentId = userCreditModel.TalentId;
                context.SaveChanges();
                return new GenericActionResult<UserCredit>(true, "User credit updated successfully.", credit);
            }
            catch (Exception) { return new GenericActionResult<UserCredit>("Failed to update credit, please try again or contact the administrator."); }
        }

        public GenericActionResult<UserCredit> DeleteUserCreditById(int creditId)
        {
            try
            {
                UserCredit credit = context.UserCredits.Find(creditId);
                credit.IsDeleted = true;
                context.SaveChanges();
                return new GenericActionResult<UserCredit>(true,"User credit deleted successfully", credit);
            }
            catch (Exception) { return new GenericActionResult<UserCredit>("Failed to delete credit, please try again or contact the administrator."); }

        }

        public GenericActionResult<List<UserCreditResponseModel>> GetUserCreditByUserId(string userId)
        {
            try
            {
                var credits = context.UserCredits.Where(a => a.UserId.Equals(userId) && !a.IsDeleted).ToList();
               return GenericActionResult<List<UserCreditResponseModel>>.FromObject(credits.Select(a=>new ObjectConverterManager(context,userManager).ToUserCreditResponseModel(a)).ToList());
            }
            catch (Exception) { return new GenericActionResult<List<UserCreditResponseModel>>("Failed to get credits, please try again or contact the administrator."); }
        }

        public GenericActionResult<List<UserCreditResponseModel>> GetAllUserCredits()
        {
            try
            {
                var credits =context.UserCredits.Where(a => !a.IsDeleted).ToList();
                return GenericActionResult<List<UserCreditResponseModel>>.FromObject(credits.Select(
                    a => new ObjectConverterManager(context, userManager).ToUserCreditResponseModel(a)).ToList());
            }
            catch (Exception) { return new GenericActionResult<List<UserCreditResponseModel>>("Failed to get credits, please try again or contact the administrator."); }
        }
    }
}
