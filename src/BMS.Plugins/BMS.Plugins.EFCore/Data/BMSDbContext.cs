using BMS.CoreBusiness.Entities;
using BMS.CoreBusiness.Entities.Membership;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BMS.Plugins.EFCore.Data
{
    public sealed class BMSDbContext : IdentityDbContext<ApplicationUser>
    {
        public BMSDbContext(DbContextOptions<BMSDbContext> options) : base(options)
        {
        }

        public DbSet<DevTask> Tasks { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DevTask>().Property(p => p.EstimatedHours).HasPrecision(8, 2);
            modelBuilder.Entity<DevTask>().Property(p => p.ActualHours).HasPrecision(8, 2);
        }
    }
}
