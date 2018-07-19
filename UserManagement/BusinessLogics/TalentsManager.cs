
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
                return new GenericActionResult<Talent>("Failed to save talent, try again.");
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
            catch (Exception exception)
            {
                return new GenericActionResult<Talent>(exception.Message);
            }
        }
    }
}
