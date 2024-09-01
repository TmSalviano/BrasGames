using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrasGames.Model.ServiceModels;
using Microsoft.EntityFrameworkCore;

namespace BrasGames.Data
{
    public class ServiceDbContext : DbContext
    {
        public ServiceDbContext(DbContextOptions<ServiceDbContext> opt) : base(opt) {

        }

        public DbSet<Model.ServiceModels.Console> Consoles { get; set; } = default!;
        public DbSet<Game> Games { get; set;} = default!;
        public DbSet<Controller> Controllers{ get; set; } = default!;

    }
}