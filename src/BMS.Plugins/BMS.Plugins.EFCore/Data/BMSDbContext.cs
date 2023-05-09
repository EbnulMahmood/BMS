using BMS.CoreBusiness.Entities;
using BMS.CoreBusiness.Entities.Membership;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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
            builder.Entity<Project>()
                .HasIndex(p => p.Name)
                .HasDatabaseName("Index_Name")
                .IsUnique();

            builder.Entity<DevTask>()
                .HasOne<Project>()
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(d => d.ProjectId);

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

            base.OnModelCreating(builder);
        }

        public override void Dispose()
        {
            Debug.WriteLine($"{ContextId} context disposed.");
            base.Dispose();
        }

        public override ValueTask DisposeAsync()
        {
            Debug.WriteLine($"{ContextId} context disposed async.");
            return base.DisposeAsync();
        }
    }
}
