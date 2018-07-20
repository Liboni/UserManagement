
namespace UserManagement.BusinessLogics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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
            return context.BusinessTypes.Where(a=>!a.IsDeleted);
        }

        public GenericActionResult<BusinessTypeModel> GetBusinessType(int id)
        {
            try
            {
                return new GenericActionResult<BusinessTypeModel>(true,"", ObjectConverterManager.ToBusinessTypeModel(context.BusinessTypes.SingleOrDefaultAsync(m => m.Id == id && !m.IsDeleted).Result));
 
             }
            catch (Exception)
            {
                return new GenericActionResult<BusinessTypeModel>("Failed to get business type, please try again or contact the administrator.");
            }
        }

        public async Task<GenericActionResult<BusinessTypeModel>> UpdateBusinessType(BusinessType businessType)
        {
            context.Entry(businessType).State = EntityState.Modified;
            try
            {
                await context.SaveChangesAsync();
                return new GenericActionResult<BusinessTypeModel>(true,"Business type updated successfully", ObjectConverterManager.ToBusinessTypeModel(businessType));
            }
            catch (DbUpdateConcurrencyException)
            {
                return new GenericActionResult<BusinessTypeModel>("Failed to update business type, please try again or contact the administrator.");
            }
        }

        public async Task<GenericActionResult<BusinessTypeModel>> AddBusinessTypeModel(BusinessType businessType)
        {
            try
            {
                context.BusinessTypes.Add(businessType);
                await context.SaveChangesAsync();
                return new GenericActionResult<BusinessTypeModel>(true,"Business type saved successfully", ObjectConverterManager.ToBusinessTypeModel(businessType));
            }
            catch (Exception)
            {
                return new GenericActionResult<BusinessTypeModel>("Failed to save business type, please try again or contact the administrator.");
            }
        }

        public async Task<GenericActionResult<BusinessTypeModel>> DeleteBusinessTypeModel(int id)
        {
            try
            {
                var businessType = await context.BusinessTypes.SingleOrDefaultAsync(m => m.Id == id);
                if (businessType == null) return new GenericActionResult<BusinessTypeModel>();
                businessType.IsDeleted=true;
                await context.SaveChangesAsync();
                return new GenericActionResult<BusinessTypeModel>(true,"Business type deleted successfully", ObjectConverterManager.ToBusinessTypeModel(businessType));
            }
            catch (Exception)
            {
                return new GenericActionResult<BusinessTypeModel>("Failed to delete business type, please try again or contact the administrator.");
            }
        }

    }
}
