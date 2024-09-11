using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using BrasGames.Data;
using BrasGames.Identity.Data;
using BrasGames.Identity.Models;
using BrasGames.Identity.Service;
using BrasGames.Model.BusinessModels;
using BrasGames.Model.DTO.BusinessDTO;
using BrasGames.Model.DTO.ServiceDTO;
using BrasGames.Model.ServiceModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// !!! DO NOT FORGET TO REMOVE PERSONAL INFORMATION WHEN COMMITING TO REPO !!!
//Activating Identity API
builder.Services.AddIdentityApiEndpoints<User>().AddEntityFrameworkStores<UsersDbContext>();

//Connecting to the Databases
builder.Services.AddDbContext<BusinessDbContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("BusinessDbConnection")));
builder.Services.AddDbContext<ServiceDbContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("ServiceDbConnection")));
builder.Services.AddDbContext<UsersDbContext>(opt => opt.UseInMemoryDatabase("Users"));

//Registering HTTP Method Services
builder.Services.AddScoped(typeof(BasicRESTService<>));
builder.Services.AddScoped(typeof(BasicRESTBusiness<>));
builder.Services.AddScoped(typeof(ComplexRESTService));

//Identity Service Registration
builder.Services.AddScoped(typeof(IdentityAddition));

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

if (builder.Environment.IsDevelopment()) {
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
}

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
   
}

//Identity
var identity = app.MapGroup("/identity");
identity.MapIdentityApi<User>();

identity.MapPost("/logout", async(IdentityAddition service, [FromBody] object? none = null) => {
    return await service.Logout(none);
}).RequireAuthorization();


//Service
var service = app.MapGroup("/service");

var controller = service.MapGroup("/controller"); //Working

//Controller: HTTP Basic Methods
controller.MapGet("/", async (BasicRESTService<ControllerDTO> basicRESTService) => {
    return await basicRESTService.GetAll();
});
controller.MapGet("/{id:int}", async (int id, BasicRESTService<ControllerDTO> basicRESTService) => {
    return await basicRESTService.GetId(id);
});
controller.MapPost("/", async ([FromBody] ControllerDTO controller, BasicRESTService<ControllerDTO> basicRESTService) => {
    return await basicRESTService.PostModel(controller);
}).RequireAuthorization();
controller.MapPut("/{id}", async (int id, [ FromBody ] ControllerDTO controller, BasicRESTService<ControllerDTO> basicRESTService) => {
    return await basicRESTService.PutModel(id, controller);
});
controller.MapDelete("/{id}", async (int id, BasicRESTService<ControllerDTO> basicRESTService) => {
    return await basicRESTService.DeleteModel(id);
});
controller.MapDelete("/", async (BasicRESTService<ControllerDTO> basicRESTService) => {
    return await basicRESTService.DeleteAllCModels();
});

//Controller: Http Query Methods
controller.MapGet("/query/", async(
    ComplexRESTService complexRESTService, string? nameSearch, string? typeSearch, int? releaseYearSearch, float priceLowerBound = 0, float priceUpperBound = float.MaxValue) => {
        return await complexRESTService.GetFilteredControllers(nameSearch, typeSearch, releaseYearSearch, priceLowerBound, priceUpperBound);
    } 
);

controller.MapDelete("/query/", async(
    ComplexRESTService complexRESTService, string? nameSearch, string? typeSearch, int? releaseYearSearch, float priceLowerBound = 0, float priceUpperBound = float.MaxValue) => {
        return await complexRESTService.DeleteFilteredControllers(nameSearch, typeSearch, releaseYearSearch, priceLowerBound, priceUpperBound);
    } 
);

var game = service.MapGroup("/game"); //Working

//Game: HTTP Basic Methods
game.MapGet("/", async (BasicRESTService<GameDTO> basicRESTService) => {
    return await basicRESTService.GetAll();
});
game.MapGet("/{id:int}", async (int id, BasicRESTService<GameDTO> basicRESTService) => {
    return await basicRESTService.GetId(id);
});
game.MapPost("/", async ( [FromBody] GameDTO game, BasicRESTService<GameDTO> basicRESTService) => {
    return await basicRESTService.PostModel(game);
});
game.MapPut("/{id}", async (int id, [ FromBody ] GameDTO game, BasicRESTService<GameDTO> basicRESTService) => {
    return await basicRESTService.PutModel(id, game);
});
game.MapDelete("/{id}", async (int id, BasicRESTService<GameDTO> basicRESTService) => {
    return await basicRESTService.DeleteModel(id);
});
game.MapDelete("/", async (BasicRESTService<GameDTO> basicRESTService) => {
    return await basicRESTService.DeleteAllCModels();
});

//Game: Http Query Methods
game.MapGet("/query/", async(
    ComplexRESTService complexRESTService, string? nameSearch, string? ageRestrictionSearch, string? genreSearch, float priceLowerBound = 0, float priceUpperBound = float.MaxValue) => {
        return await complexRESTService.GetFilteredGames(nameSearch, ageRestrictionSearch, genreSearch, priceLowerBound, priceUpperBound);
    } 
);

game.MapDelete("/query/", async(
    ComplexRESTService complexRESTService, string? nameSearch, string? ageRestrictionSearch, string? genreSearch, float priceLowerBound = 0, float priceUpperBound = float.MaxValue) => {
        return await complexRESTService.DeleteFilteredGames(nameSearch, ageRestrictionSearch, genreSearch, priceLowerBound, priceUpperBound);
    } 
);

var console = service.MapGroup("/console");

//Console: HTTP Basic Methods. Using ConsoleModel as ServiceModels.Console. - Working
console.MapGet("/", async (BasicRESTService<ConsoleDTO> basicRESTService) => {
    return await basicRESTService.GetAll();
});
console.MapGet("/{id:int}", async (int id, BasicRESTService<ConsoleDTO> basicRESTService) => {
    return await basicRESTService.GetId(id);
});
console.MapPost("/", async ( [FromBody] ConsoleDTO console, BasicRESTService<ConsoleDTO> basicRESTService) => {
    return await basicRESTService.PostModel(console);
});
console.MapPut("/{id}", async (int id, [ FromBody ] ConsoleDTO console, BasicRESTService<ConsoleDTO> basicRESTService) => {
    return await basicRESTService.PutModel(id, console);
});
console.MapDelete("/{id}", async (int id, BasicRESTService<ConsoleDTO> basicRESTService) => {
    return await basicRESTService.DeleteModel(id);
});
console.MapDelete("/", async (BasicRESTService<ConsoleDTO> basicRESTService) => {
    return await basicRESTService.DeleteAllCModels();
});

//Console Http Query Methods
console.MapGet("/query/", async(
    ComplexRESTService complexRESTService, string? nameSearch, string? typeSearch, DateTime? releaseYearSearch, float priceLowerBound = 0, float priceUpperBound = float.MaxValue) => {
        return await complexRESTService.GetFilteredConsoles(nameSearch, typeSearch, releaseYearSearch, priceLowerBound, priceUpperBound);
    } 
);

console.MapDelete("/query/", async(
    ComplexRESTService complexRESTService, string? nameSearch, string? typeSearch, DateTime? releaseYearSearch, float priceLowerBound = 0, float priceUpperBound = float.MaxValue) => {
        return await complexRESTService.DeleteFilteredConsoles(nameSearch, typeSearch, releaseYearSearch, priceLowerBound, priceUpperBound);
    } 
);

var business = app.MapGroup("/business");

var employee = business.MapGroup("/employee");

//Employee: HTTP Basic Methods. - Working
employee.MapGet("/", async (BasicRESTBusiness<EmployeeDTO> basicRESTBusiness) => {
    return await basicRESTBusiness.GetAll();
});
employee.MapGet("/{id:int}", async (int id, BasicRESTBusiness<EmployeeDTO> basicRESTBusiness) => {
    return await basicRESTBusiness.GetId(id);
});
employee.MapPost("/", async ( [FromBody] EmployeeDTO employee, BasicRESTBusiness<EmployeeDTO> basicRESTBusiness) => {
    return await basicRESTBusiness.PostModel(employee);
});
employee.MapPut("/{id}", async (int id, EmployeeDTO employee, BasicRESTBusiness<EmployeeDTO> basicRESTBusiness) => {
    return await basicRESTBusiness.PutModel(id, employee);
});
employee.MapDelete("/{id}", async (int id, BasicRESTBusiness<EmployeeDTO> basicRESTBusiness) => {
    return await basicRESTBusiness.DeleteModel(id);
});
employee.MapDelete("/", async (BasicRESTBusiness<EmployeeDTO> basicRESTBusiness) => {
    return await basicRESTBusiness.DeleteAllModels();
});

//Employees Query Methods
employee.MapGet("/query/", async(
    ComplexRESTService complexRESTService, string? nameSearch,  bool? isFiredSearch, int ageLowerBound = 0, int ageUpperBound = int.MaxValue, int yearsWorkedLowerBound = 0, int yearsWorkedUpperBound = int.MaxValue, float salaryLowerBound = 0, float salaryUpperBound = float.MaxValue) => {
        return await complexRESTService.GetFilteredEmployees(nameSearch, ageLowerBound, ageUpperBound, yearsWorkedLowerBound, yearsWorkedUpperBound, salaryLowerBound, salaryUpperBound, isFiredSearch);
    } 
);

employee.MapDelete("/query/", async(
    ComplexRESTService complexRESTService, string? nameSearch,  bool? isFiredSearch, int ageLowerBound = 0, int ageUpperBound = int.MaxValue, int yearsWorkedLowerBound = 0, int yearsWorkedUpperBound = int.MaxValue, float salaryLowerBound = 0, float salaryUpperBound = float.MaxValue) => {
        return await complexRESTService.DeleteFilteredEmployees(nameSearch, ageLowerBound, ageUpperBound, yearsWorkedLowerBound, yearsWorkedUpperBound, salaryLowerBound, salaryUpperBound, isFiredSearch);
    } 
);

var agenda = business.MapGroup("/agenda");

//DayStats: HTTP Basic Methods. - Working
agenda.MapGet("/", async (BasicRESTBusiness<DayStatsDTO> basicRESTBusiness) => {
    return await basicRESTBusiness.GetAll();
});
agenda.MapGet("/{id:int}", async (int id, BasicRESTBusiness<DayStatsDTO> basicRESTBusiness) => {
    return await basicRESTBusiness.GetId(id);
});
agenda.MapPost("/", async ( [FromBody] DayStatsDTO dayStats, BasicRESTBusiness<DayStatsDTO> basicRESTBusiness) => {
    return await basicRESTBusiness.PostModel(dayStats);
});
agenda.MapPut("/{id}", async (int id, [ FromBody ] DayStatsDTO dayStats, BasicRESTBusiness<DayStatsDTO> basicRESTBusiness) => {
    return await basicRESTBusiness.PutModel(id, dayStats);
});
agenda.MapDelete("/{id}", async (int id, BasicRESTBusiness<DayStatsDTO> basicRESTBusiness) => {
    return await basicRESTBusiness.DeleteModel(id);
});
agenda.MapDelete("/", async (BasicRESTBusiness<DayStatsDTO> basicRESTBusiness) => {
    return await basicRESTBusiness.DeleteAllModels();
});

//Agenda Http Query Methods
agenda.MapGet("/query/", async(
    ComplexRESTService complexRESTService, DateTime? dateSearch, int totalConsumerLowerBound = 0, int totalConsumerUpperBound = int.MaxValue, int totalProfitLowerBound = 0, int totalProfitUpperBound = int.MaxValue, int totalCostLowerBound = 0, int totalCostUpperBound = int.MaxValue) => {
        return await complexRESTService.GetFilteredAgenda(dateSearch, totalConsumerLowerBound, totalConsumerUpperBound, totalProfitLowerBound, totalProfitUpperBound, totalCostLowerBound, totalCostUpperBound);
    } 
);

agenda.MapDelete("/query/", async(
    ComplexRESTService complexRESTService, DateTime? dateSearch, int totalConsumerLowerBound = 0, int totalConsumerUpperBound = int.MaxValue, int totalProfitLowerBound = 0, int totalProfitUpperBound = int.MaxValue, int totalCostLowerBound = 0, int totalCostUpperBound = int.MaxValue) => {
        return await complexRESTService.DeleteFilteredAgenda(dateSearch, totalConsumerLowerBound, totalConsumerUpperBound, totalProfitLowerBound, totalProfitUpperBound, totalCostLowerBound, totalCostUpperBound);
    } 
);

app.Run();

