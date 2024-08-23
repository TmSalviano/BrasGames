using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrasGames.Model;
using Microsoft.EntityFrameworkCore;

namespace BrasGames.Data
{
    public class ServiceDbContext : DbContext
    {
        public ServiceDbContext(DbContextOptions<ServiceDbContext> opt) : base(opt) {

        }

        DbSet<ServiceTestModel> serviceTestModels = default!;
    }
}