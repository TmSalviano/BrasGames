using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BrasGames.Identity.Data
{
    public class UsersDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>  
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base (options) {

        }

        public override DbSet<IdentityUser> Users { get; set;} = default!;
    }
}