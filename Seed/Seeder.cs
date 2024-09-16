using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrasGames.Data;
using BrasGames.Identity.Data;
using BrasGames.Model.BusinessModels;
using BrasGames.Model.ServiceModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Console = BrasGames.Model.ServiceModels.Console;

namespace BrasGames.Seed
{
    public static class Seeder
    {
        public static async Task  SeedBusinessDb(IServiceProvider serviceProvider) {
            using (var context = new BusinessDbContext(serviceProvider.GetRequiredService<DbContextOptions<BusinessDbContext>>())) 
            {
                await SeedEmployees(context);
                await SeedAgenda(context);
            }
        }

        public static async Task SeedEmployees(BusinessDbContext context) {
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
        public static async Task SeedAgenda(BusinessDbContext context) {
            if (context.Agenda.Any()) 
                    return;
            var agendas = new List<DayStats>() {
                new DayStats() {
                    Day = DateTime.Now.AddDays(1),
                    Id = 0,
                    TotalConsumers = 1000,
                    TotalCost = 2000,
                    TotalProfit = 500,
                },
                new DayStats() {
                    Day = DateTime.Now.AddDays(2),
                    Id = 1,
                    TotalConsumers = 1500,
                    TotalCost = 1000,
                    TotalProfit = 750,
                },
                new DayStats() {
                    Day = DateTime.Now.AddDays(3),
                    Id = 2,
                    TotalConsumers = 2000,
                    TotalCost = 1000,
                    TotalProfit = 1500,
                },
            };  
            await context.Agenda.AddRangeAsync(agendas);
            await context.SaveChangesAsync();
        }

        public static async Task SeedServiceDb(IServiceProvider serviceProvider) {
            using (var context = new ServiceDbContext(serviceProvider.GetRequiredService<DbContextOptions<ServiceDbContext>>()))   {
                await SeedControllers(context);
                await SeedGames(context);
                await SeedConsoles(context);
            }
        }

        public static async Task SeedControllers(ServiceDbContext context) {
            if (context.Controllers.Any()) 
                    return;
            var controllers = new List<Controller>() {
                new Controller() {
                    Name = "Dualshock maxHyperMegaUltra",
                    Id = 0,
                    Price = 2000,
                    Type = "PS4",
                    Year = 2022,
                },
                new Controller() {
                    Name = "Duke",
                    Id = 1,
                    Price = 2000,
                    Type = "XBOX",
                    Year = 2022,
                },
                new Controller() {
                    Name = "Dualsense Hyper Fucking Ultra Mega Power",
                    Id = 2,
                    Price = 5000,
                    Type = "PS4S",
                    Year = 2022,
                },
            };  
            await context.Controllers.AddRangeAsync(controllers);
            await context.SaveChangesAsync();
        }
        public static async Task SeedGames(ServiceDbContext context) {
            if (context.Games.Any()) 
                    return;
            var games = new List<Game>() {
                new Game() {
                    Id = 0,
                    Name = "Tonico e Tinoco, a Jornada.",
                    AgeRestriction = "18+",
                    Genre = "Adventure",
                    Price = 200,
                },
                new Game() {
                    Id = 1,
                    Name = "Nasci no Rio de Janeiro!",
                    AgeRestriction = "21+",
                    Genre = "Horror",
                    Price = 250,
                },
                new Game() {
                    Id = 2,
                    Name = "Garota de Ipanema",
                    AgeRestriction = "12+",
                    Genre = "Light Novel",
                    Price = 200,
                },
            };  
            await context.Games.AddRangeAsync(games);
            await context.SaveChangesAsync();
        }
        public static async Task SeedConsoles(ServiceDbContext context) {
            if (context.Consoles.Any()) 
                    return;
            var consoles = new List<Console>() {
                new Console() {
                    Id = 0,
                    Name = "PS5",
                    Price = 4000,
                    ReleaseYear = DateTime.Now.AddDays(1),
                    Type = "Playstation",
                },
                new Console() {
                    Id = 1,
                    Name = "Xbox 360",
                    Price = 1000,
                    ReleaseYear = DateTime.Now.AddDays(2),
                    Type = "Playstation",
                },
                new Console() {
                    Id = 2,
                    Name = "PS4",
                    Price = 2000,
                    ReleaseYear = DateTime.Now.AddDays(3),
                    Type = "Xbox",
                },
            };  
            await context.Consoles.AddRangeAsync(consoles);
            await context.SaveChangesAsync();
        }
    }
}