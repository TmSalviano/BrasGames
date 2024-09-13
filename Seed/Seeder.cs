using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrasGames.Data;
using BrasGames.Identity.Data;
using BrasGames.Model.BusinessModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BrasGames.Seed
{
    public static class Seeder
    {
        public static async Task  SeedEmployees(IServiceProvider serviceProvider) {
            using (var context = new BusinessDbContext(serviceProvider.GetRequiredService<DbContextOptions<BusinessDbContext>>())) 
            {
                if (context.Employees.Any()) 
                    return;
                var employees = new List<Employee>() {
                    new Employee() {
                        Name = "Tiago",
                        Age = 23,
                        Email = "tiago.salviano@outlook.com",
                        EndOfContract = DateTime.Now.AddYears(5),
                        isFired = false,
                        Password = "Mypassword!23",
                        Salary = 300000f,
                        Sex = Model.SexType.Male,
                        YearsWorked = 5,
                    },
                    new Employee() {
                        Name = "Joanna",
                        Age = 90,
                        Email = "joanna@hotmail.com",
                        EndOfContract = DateTime.Now.AddYears(1),
                        isFired = true,
                        Password = "Oo!23!23!23",
                        Salary = 600000,
                        Sex = Model.SexType.Female,
                        YearsWorked = 76,
                    },
                    new Employee() {
                        Name = "John",
                        Age = 30,
                        Email = "JohnDoe@outlook.com",
                        EndOfContract = DateTime.Now.AddYears(4),
                        isFired = false,
                        Password = "JohnDoe!23",
                        Salary = 34,
                        Sex = Model.SexType.Male,
                        YearsWorked = 34,
                    },
                };   
                await context.Employees.AddRangeAsync(employees);
                await context.SaveChangesAsync();
            }
        }
    }
}