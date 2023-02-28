using BMS.CoreBusiness.Entities;
using Microsoft.EntityFrameworkCore;

namespace BMS.Plugins.BMS.Plugins.EFCore.Data
{
    public sealed class BMSDbContext : DbContext
    {
        public BMSDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<DevTask> Tasks { get; set; }
    }
}
