
namespace UserManagement.BusinessLogics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using UserManagement.Data;
    using UserManagement.LocalObjects;

    public class TalentsManager
    {
        private readonly ApplicationDbContext context;

        public TalentsManager(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<Talent> GetAllTalents()
        {
            return context.Talents.ToList();
        }

        public Talent GetTalentById(int id)
        {
            return context.Talents.Find(id);
        }

        public GenericActionResult<string> AddTalent(string name)
        {
            try
            {
                context.Talents.Add(new Talent{Name = name});
                context.SaveChanges();
                return new GenericActionResult<string>(true,"Talent saved successfully.");

            }
            catch (Exception)
            {
                return new GenericActionResult<string>("Failed to save talent, try again.");
            }
        }

        public GenericActionResult<string> DeleteTalent(int talentId)
        {
            try
            {
                context.Talents.Remove(context.Talents.Find(talentId));
                context.SaveChanges();
                return new GenericActionResult<string>(true, "Talent deleted successfully.");

            }
            catch (Exception exception)
            {
                return new GenericActionResult<string>(exception.Message);
            }
        }
    }
}
