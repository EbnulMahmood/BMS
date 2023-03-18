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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<DevTask>()
                .HasOne<Project>()
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(d => d.ProjectId);

            builder.Entity<DevTask>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(d => d.EntryById);

            builder.Entity<DevTask>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(d => d.Responsible1Id);

            builder.Entity<DevTask>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(d => d.Responsible2Id);

            builder.Entity<DevTask>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(d => d.QAResponsibleId);

            builder.Entity<DevTask>().Property(p => p.EstimatedHours).HasPrecision(8, 2);
            builder.Entity<DevTask>().Property(p => p.ActualHours).HasPrecision(8, 2);
        }
    }
}
