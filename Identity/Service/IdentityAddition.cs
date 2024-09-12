using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace BrasGames.Identity.Service
{
    public class IdentityAddition
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public IdentityAddition(SignInManager<IdentityUser> signInManager) {
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