using System.Security.Claims;
using BrasGames.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace BrasGames.Identity.Service
{
    public class IdentityAddition
    {
        private readonly SignInManager<User> _signInManager;

        public IdentityAddition(SignInManager<User> signInManager) {
            _signInManager = signInManager;
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