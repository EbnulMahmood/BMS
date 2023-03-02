﻿using BMS.CoreBusiness.Entities;
using Microsoft.EntityFrameworkCore;

namespace BMS.Plugins.EFCore.Data
{
    public sealed class BMSDbContext : DbContext
    {
        public BMSDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<DevTask> Tasks { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}
