using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrasGames.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BrasGames.Identity.Data
{
    public class UsersDbContext : IdentityDbContext<User>  
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base (options) {

        }

        public override DbSet<User> Users { get; set;} = default!;
    }
}