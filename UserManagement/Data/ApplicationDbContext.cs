using Microsoft.EntityFrameworkCore;
using UserManagement.Models.BusinessTypeModels;

namespace UserManagement.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using UserManagement.Models;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Token> Tokens { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<ProfileImage> ProfileImages { get; set; }
        public DbSet<UserCredit> UserCredits { get; set; }
        public DbSet<Talent> Talents  { get; set; }
        public DbSet<OrganisationProfile> OrganisationProfiles { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<BusinessType> BusinessTypes { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
