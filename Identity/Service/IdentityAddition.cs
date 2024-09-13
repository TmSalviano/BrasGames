using System.Security.Claims;
using BrasGames.Data;
using BrasGames.Model.BusinessModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BrasGames.Identity.Service
{
    public class IdentityAddition
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly BusinessDbContext _businessDbContext;  

        public IdentityAddition(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, BusinessDbContext businessDbContext) {
            _signInManager = signInManager;
            _userManager = userManager;
            _businessDbContext = businessDbContext;
        }

        public async Task<IResult> LogoutAsync(object? none) {
                if (none != null)
                {
                    await _signInManager.SignOutAsync();
                    return TypedResults.Ok();
                }
                return TypedResults.Unauthorized();
        }

        public async Task<IResult> AddClientRoleAsync(object? none, HttpContext httpContext)
        {
            try
            {
                if (none != null && httpContext.User.Identity?.IsAuthenticated == true)
                {
                    var userName = httpContext.User.Identity.Name;
                    var user = await _userManager.FindByNameAsync(userName!);
                    if (user != null)
                    {
                        if (!await _userManager.IsInRoleAsync(user, "Client"))
                        {
                            var result = await _userManager.AddToRoleAsync(user, "Client");
                            if (!result.Succeeded)
                                return TypedResults.Problem("Failed to add user to the role.");
                        } else {
                            return TypedResults.BadRequest("User is Client already confirmed.");
                        }

                        await httpContext.SignOutAsync();
                        //I've spent 2 hours thinking that there was something wrong with my code but actually you need to logout and then login again to refresh the claims. RIDICULOUS!!! HOW THE FUCK AM I SUPPOSED TO KNOW THAT????
                        return TypedResults.Redirect("/identity/login");
                    }
                    return TypedResults.BadRequest("User not found.");
                }
                return TypedResults.Unauthorized();
            }
            catch (Exception ex)
            {
                return TypedResults.Problem($"An error occurred: {ex.Message}");
            }
        }

        public async Task<IResult> AddEmployeeRoleAsync(object? none, HttpContext httpContext)
        {
            try
            {
                if (none != null && httpContext.User.Identity?.IsAuthenticated == true)
                {
                    var userName = httpContext.User.Identity.Name;
                    var user = await _userManager.FindByNameAsync(userName!);
                    if (user != null)
                    {
                        var empsData = await _businessDbContext.Employees.Select(e => e.Email).ToListAsync();

                        if (!empsData.Where(email => email ==user.Email).Any() && empsData.Where(email => email == user.Email).Any()!) {
                            return TypedResults.BadRequest("Employee not registered in the database. Owner needs to add your account to the database: /business/employee.");
                        }

                        if (!await _userManager.IsInRoleAsync(user, "Employee"))
                        {
                            var result = await _userManager.AddToRoleAsync(user, "Employee");
                            if (!result.Succeeded)
                                return TypedResults.Problem("Failed to add user to the role.");
                        } else {
                            return TypedResults.BadRequest("User is Employee already confirmed.");
                        }

                        await httpContext.SignOutAsync();
                        //I've spent 2 hours thinking that there was something wrong with my code but actually you need to logout and then login again to refresh the claims. RIDICULOUS!!! HOW THE FUCK AM I SUPPOSED TO KNOW THAT????
                        return TypedResults.Redirect("/identity/login");
                    }
                    return TypedResults.BadRequest("User not found.");
                }
                return TypedResults.Unauthorized();
            }
            catch (Exception ex)
            {
                return TypedResults.Problem($"An error occurred: {ex.Message}");
            }
        }
    }   
}