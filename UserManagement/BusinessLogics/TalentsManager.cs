
namespace UserManagement.BusinessLogics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using UserManagement.Data;
    using UserManagement.LocalObjects;
    using UserManagement.Models.TalentModels;

    public class TalentsManager
    {
        private readonly ApplicationDbContext context;

        public TalentsManager(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<Talent> GetAllTalents()
        {
            return context.Talents.Where(a=>!a.IsDeleted).ToList();
        }

        public Talent GetTalentById(int id)
        {
            return context.Talents.FirstOrDefault(a => !a.IsDeleted &&a.Id==id);
        }

        public async Task<GenericActionResult<TalentModel>> UpdateTalent(TalentModel talentModel)
        {
            context.Entry(new Talent{Id = talentModel.Id,Name = talentModel.Name}).State = EntityState.Modified;
            try
            {
                await context.SaveChangesAsync();
                return new GenericActionResult<TalentModel>(true, "Talent updated successfully", talentModel);
            }
            catch (DbUpdateConcurrencyException)
            {
                return new GenericActionResult<TalentModel>("Failed to update talent, please try again or contact the administrator.");
            }
        }
        public GenericActionResult<Talent> AddTalent(string name)
        {
            try
            {
                var talent = new Talent { Name = name };
                context.Talents.Add(talent);
                context.SaveChanges();
                return new GenericActionResult<Talent>(true,"Talent saved successfully.", talent);

            }
            catch (Exception)
            {
                return new GenericActionResult<Talent>("Failed to save talent, please try again or contact the administrator.");
            }
        }

        public GenericActionResult<Talent> DeleteTalent(int talentId)
        {
            try
            {
                var talent = context.Talents.Find(talentId);
                context.Talents.Remove(talent);
                context.SaveChanges();
                return new GenericActionResult<Talent>(true, "Talent deleted successfully.", talent);

            }
            catch (Exception)
            {
                return new GenericActionResult<Talent>("Failed to delete talent, please try again or contact the administrator.");
            }
        }
    }
}
