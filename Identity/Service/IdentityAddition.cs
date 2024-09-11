using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrasGames.Identity.Data;
using BrasGames.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace BrasGames.Identity.Service
{
    public class IdentityAddition
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UsersDbContext _dbContext;

        public IdentityAddition(SignInManager<User> signInManager, UsersDbContext usersDbContext) {
            _signInManager = signInManager;
            _dbContext = usersDbContext;
        }

        public async Task<IResult> Logout(object? none) {
                if (none != null)
                {
                    await _signInManager.SignOutAsync();
                    return TypedResults.Ok();
                }
                return TypedResults.Unauthorized();
        }
    }
}