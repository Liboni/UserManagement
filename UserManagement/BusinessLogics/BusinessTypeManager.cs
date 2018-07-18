
namespace UserManagement.BusinessLogics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using UserManagement.Data;
    using UserManagement.LocalObjects;
    using UserManagement.Models.BusinessTypeModels;

    public class BusinessTypeManager
    {
        private readonly ApplicationDbContext context;

        public BusinessTypeManager(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<BusinessType> GetBusinessType()
        {
            return context.BusinessTypes;
        }

        public async Task<GenericActionResult<BusinessType>> GetBusinessType(int id)
        {
            try
            {
                return new GenericActionResult<BusinessType>(true,"", await context.BusinessTypes.SingleOrDefaultAsync(m => m.Id == id));
   
             }
            catch (Exception exception)
            {
                return new GenericActionResult<BusinessType>(exception.Message);
            }
        }

        public async Task<GenericActionResult<BusinessType>> UpdateBusinessType(BusinessType businessType)
        {
            context.Entry(businessType).State = EntityState.Modified;
            try
            {
                await context.SaveChangesAsync();
                return new GenericActionResult<BusinessType>(true,"Business type saved successfully", businessType);
            }
            catch (DbUpdateConcurrencyException exception)
            {
                return new GenericActionResult<BusinessType>(exception.Message);
            }
        }

        public async Task<GenericActionResult<BusinessType>> AddBusinessTypeModel(BusinessType businessType)
        {
            try
            {
                context.BusinessTypes.Add(businessType);
                await context.SaveChangesAsync();
                return new GenericActionResult<BusinessType>(true,"Business type saved successfully", businessType);
            }
            catch (Exception exception)
            {
                return new GenericActionResult<BusinessType>(exception.Message);
            }
        }

        public async Task<GenericActionResult<BusinessType>> DeleteBusinessTypeModel(int id)
        {
            try
            {
                var businessType = await context.BusinessTypes.SingleOrDefaultAsync(m => m.Id == id);
                if (businessType == null) return new GenericActionResult<BusinessType>();
                context.BusinessTypes.Remove(businessType);
                await context.SaveChangesAsync();
                return new GenericActionResult<BusinessType>(true,"Business type deleted successfully",businessType);
            }
            catch (Exception exception)
            {
                return new GenericActionResult<BusinessType>(exception.Message);
            }
        }

    }
}
