using Microsoft.EntityFrameworkCore;
using SmartStudy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartStudy.Infrastructure.Persistence
{
    public class ApplicationContextDb : DbContext
    {
        public ApplicationContextDb(DbContextOptions<ApplicationContextDb> options) : base(options) {}

        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ApplicationContextDb).Assembly
            );
        }
    }
}
