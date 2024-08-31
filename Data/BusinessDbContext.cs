using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using BrasGames.Model;
using BrasGames.Model.BusinessModels;
using Microsoft.EntityFrameworkCore;

namespace BrasGames.Data
{
    public class BusinessDbContext : DbContext
    {
        public BusinessDbContext(DbContextOptions<BusinessDbContext> opt) : base(opt) {

        }

        public DbSet<DayStats> Agenda { get; set; } = default!;
        public DbSet<Employee> Employees { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<DayStats>().HasKey(dayStats => dayStats.Id);
            modelBuilder.Entity<DayStats>().HasIndex(dayStats => dayStats.Day).IsUnique();
        }
    }
}