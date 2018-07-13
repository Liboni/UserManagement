﻿
namespace UserManagement.BusinessLogics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using UserManagement.Data;
    using UserManagement.LocalObjects;
    using UserManagement.Models.UserCreditModels;

    public class UserCreditsManager
    {
        private readonly ApplicationDbContext context;

        public UserCreditsManager(ApplicationDbContext context)
        {
            this.context = context;
        }

        public GenericActionResult<string> SaveUserCredit(UserCreditModel userCreditModel)
        {
            try
            {
                context.UserCredits.Add(new UserCredit
                {
                    Production = userCreditModel.Production,
                    TalentId = userCreditModel.TalentId,
                    UserId = userCreditModel.UserId,
                    IsDeleted = false,
                    DateCreated = DateTime.Now
                });
                context.SaveChanges();
                return new GenericActionResult<string>(true, "");
            }
            catch (Exception exception) { return new GenericActionResult<string>(exception.Message); }
        }

        public GenericActionResult<string> UpdateUserCredit(UserCreditModel userCreditModel)
        {
            try
            {
                UserCredit  credit = context.UserCredits.Find(userCreditModel.Id);
                credit.Production = userCreditModel.Production;
                credit.TalentId = userCreditModel.TalentId;
                context.SaveChanges();
                return new GenericActionResult<string>(true, "");
            }
            catch (Exception exception) { return new GenericActionResult<string>(exception.Message); }
        }

        public GenericActionResult<string> DeleteUserCreditById(int creditId)
        {
            try
            {
                UserCredit credit = context.UserCredits.Find(creditId);
                credit.IsDeleted = true;
                context.SaveChanges();
                return new GenericActionResult<string>(true,"");
            }
            catch (Exception exception) { return GenericActionResult<string>.FromObject(exception.Message); }

        }

        public GenericActionResult<UserCreditModel> GetUserCreditByUserId(string userId)
        {
            try
            {
               return GenericActionResult<UserCreditModel>.FromObject(context.UserCredits.Where(a => a.UserId.Equals(userId)&& !a.IsDeleted).Select(a => new UserCreditModel { UserId = userId, Production = a.Production, TalentId = a.TalentId ,Id = a.Id}).FirstOrDefault());
            }
            catch (Exception exception) { return new GenericActionResult<UserCreditModel>(exception.Message); }
        }

        public GenericActionResult<List<UserCreditModel>> GetAllUserCredits()
        {
            try
            {
                return GenericActionResult<List<UserCreditModel>>.FromObject(context.UserCredits.Where(a=> !a.IsDeleted).Select(a => new UserCreditModel { UserId = a.UserId, Production = a.Production, TalentId = a.TalentId, Id = a.Id}).ToList());
            }
            catch (Exception exception) { return new GenericActionResult<List<UserCreditModel>>(exception.Message); }
        }
    }
}
