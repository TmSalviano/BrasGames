using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using BrasGames.Data;
using BrasGames.Model.BusinessModels;
using BrasGames.Model.ServiceModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using ConsoleModel = BrasGames.Model.ServiceModels.Console;

var builder = WebApplication.CreateBuilder(args);

// TypedResult Methods will be used for HTTP methods with complex LINQ query operations.
// Dont forget the DTO's 

//Connecting to the Databases
builder.Services.AddDbContext<BusinessDbContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("BusinessDbConnection")));
builder.Services.AddDbContext<ServiceDbContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("ServiceDbConnection")));
builder.Services.AddScoped(typeof(BasicRESTService<>));
builder.Services.AddScoped(typeof(BasicRESTBusiness<>));
builder.Services.AddScoped(typeof(ComplexRESTService));

if (builder.Environment.IsDevelopment()) {
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
   
}

var service = app.MapGroup("/service");

var controller = service.MapGroup("/controller"); //Working

//Controller: HTTP Basic Methods
controller.MapGet("/", async (BasicRESTService<Controller> basicRESTService) => {
    return await basicRESTService.GetAll();
});
controller.MapGet("/{id:int}", async (int id, BasicRESTService<Controller> basicRESTService) => {
    return await basicRESTService.GetId(id);
});
controller.MapPost("/", async (Controller controller, BasicRESTService<Controller> basicRESTService) => {
    return await basicRESTService.PostModel(controller);
});
controller.MapPut("/{id}", async (int id, Controller controller, BasicRESTService<Controller> basicRESTService) => {
    return await basicRESTService.PutModel(id, controller);
});
controller.MapDelete("/{id}", async (int id, BasicRESTService<Controller> basicRESTService) => {
    return await basicRESTService.DeleteModel(id);
});
controller.MapDelete("/", async (BasicRESTService<Controller> basicRESTService) => {
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
game.MapGet("/", async (BasicRESTService<Game> basicRESTService) => {
    return await basicRESTService.GetAll();
});
game.MapGet("/{id:int}", async (int id, BasicRESTService<Game> basicRESTService) => {
    return await basicRESTService.GetId(id);
});
game.MapPost("/", async (Game game, BasicRESTService<Game> basicRESTService) => {
    return await basicRESTService.PostModel(game);
});
game.MapPut("/{id}", async (int id, Game game, BasicRESTService<Game> basicRESTService) => {
    return await basicRESTService.PutModel(id, game);
});
game.MapDelete("/{id}", async (int id, BasicRESTService<Game> basicRESTService) => {
    return await basicRESTService.DeleteModel(id);
});
game.MapDelete("/", async (BasicRESTService<Game> basicRESTService) => {
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
console.MapGet("/", async (BasicRESTService<ConsoleModel> basicRESTService) => {
    return await basicRESTService.GetAll();
});
console.MapGet("/{id:int}", async (int id, BasicRESTService<ConsoleModel> basicRESTService) => {
    return await basicRESTService.GetId(id);
});
console.MapPost("/", async (ConsoleModel console, BasicRESTService<ConsoleModel> basicRESTService) => {
    return await basicRESTService.PostModel(console);
});
console.MapPut("/{id}", async (int id, ConsoleModel console, BasicRESTService<ConsoleModel> basicRESTService) => {
    return await basicRESTService.PutModel(id, console);
});
console.MapDelete("/{id}", async (int id, BasicRESTService<ConsoleModel> basicRESTService) => {
    return await basicRESTService.DeleteModel(id);
});
console.MapDelete("/", async (BasicRESTService<ConsoleModel> basicRESTService) => {
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
employee.MapGet("/", async (BasicRESTBusiness<Employee> basicRESTBusiness) => {
    return await basicRESTBusiness.GetAll();
});
employee.MapGet("/{id:int}", async (int id, BasicRESTBusiness<Employee> basicRESTBusiness) => {
    return await basicRESTBusiness.GetId(id);
});
employee.MapPost("/", async (Employee employee, BasicRESTBusiness<Employee> basicRESTBusiness) => {
    return await basicRESTBusiness.PostModel(employee);
});
employee.MapPut("/{id}", async (int id, Employee employee, BasicRESTBusiness<Employee> basicRESTBusiness) => {
    return await basicRESTBusiness.PutModel(id, employee);
});
employee.MapDelete("/{id}", async (int id, BasicRESTBusiness<Employee> basicRESTBusiness) => {
    return await basicRESTBusiness.DeleteModel(id);
});
employee.MapDelete("/", async (BasicRESTBusiness<Employee> basicRESTBusiness) => {
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
agenda.MapGet("/", async (BasicRESTBusiness<DayStats> basicRESTBusiness) => {
    return await basicRESTBusiness.GetAll();
});
agenda.MapGet("/{id:int}", async (int id, BasicRESTBusiness<DayStats> basicRESTBusiness) => {
    return await basicRESTBusiness.GetId(id);
});
agenda.MapPost("/", async (DayStats dayStats, BasicRESTBusiness<DayStats> basicRESTBusiness) => {
    return await basicRESTBusiness.PostModel(dayStats);
});
agenda.MapPut("/{id}", async (int id, DayStats dayStats, BasicRESTBusiness<DayStats> basicRESTBusiness) => {
    return await basicRESTBusiness.PutModel(id, dayStats);
});
agenda.MapDelete("/{id}", async (int id, BasicRESTBusiness<DayStats> basicRESTBusiness) => {
    return await basicRESTBusiness.DeleteModel(id);
});
agenda.MapDelete("/", async (BasicRESTBusiness<DayStats> basicRESTBusiness) => {
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

