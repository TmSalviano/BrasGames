using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using BrasGames.Model;
using Microsoft.EntityFrameworkCore;

namespace BrasGames.Data
{
    public class BusinessDbContext : DbContext
    {
        public BusinessDbContext(DbContextOptions<BusinessDbContext> opt) : base(opt) {

        }

        public DbSet<BusinessTestModel> businessTestModels { get; set; } = default!;
    }
}