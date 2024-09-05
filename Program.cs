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

app.Run();

//Service Models Complex Query Methods
    // Console
static async Task<IResult> GetFilteredConsoles( ServiceDbContext db,
    string? nameSearch = null, string? typeSearch = null, 
    DateTime? releaseYearSearch = null, float priceLowerBound = 0, float priceUpperBound = float.MaxValue) {

    var collection = db.Consoles.AsQueryable();
    
    if (nameSearch != null) {
        collection = collection.Where(model => model.Name.Contains(nameSearch));
    }
    if (typeSearch != null) {
        collection = collection.Where(model => model.Type.Contains(typeSearch));
    }
    if (releaseYearSearch != null) {
        collection = collection.Where(model => model.ReleaseYear == releaseYearSearch);
    }
    if (priceLowerBound != 0 || priceUpperBound < float.MaxValue) {
        collection = collection.Where(model => model.Price >= priceLowerBound && model.Price <= priceUpperBound);
    }

    return TypedResults.Ok(await collection.ToListAsync());
}

static async Task<IResult> DeleteFilteredConsoles(ServiceDbContext db,
    string? nameSearch = null, string? typeSearch = null, 
    DateTime? releaseYearSearch = null, float priceLowerBound = 0, float priceUpperBound = float.MaxValue) {
    
    var collection = db.Consoles.AsQueryable();

    if (nameSearch != null) {
        collection = collection.Where(model => model.Name.Contains(nameSearch));
    }
    if (typeSearch != null) {
        collection = collection.Where(model => model.Type.Contains(typeSearch));
    }
    if (releaseYearSearch != null) {
        collection = collection.Where(model => model.ReleaseYear == releaseYearSearch);
    }
    if (priceLowerBound != 0 || priceUpperBound < float.MaxValue) {
        collection = collection.Where(model => model.Price >= priceLowerBound && model.Price <= priceUpperBound);
    }

    var items = await collection.ToListAsync();
    if (items.Any()) {
        db.Consoles.RemoveRange(items);
    }

    await db.SaveChangesAsync();
    return TypedResults.NoContent();
}

    // Controller
static async Task<IResult> GetFilteredControllers(ServiceDbContext db,
    string? nameSearch = null, string? typeSearch = null, 
    int? releaseYearSearch = null, float priceLowerBound = 0, float priceUpperBound = float.MaxValue) {
    
    var collection = db.Controllers.AsQueryable();
    
    if (nameSearch != null) {
        collection = collection.Where(model => model.Name.Contains(nameSearch));
    }
    if (typeSearch != null) {
        collection = collection.Where(model => model.Type.Contains(typeSearch));
    }
    if (releaseYearSearch != null) {
        collection = collection.Where(model => model.Year == releaseYearSearch);
    }
    if (priceLowerBound != 0 || priceUpperBound < float.MaxValue) {
        collection = collection.Where(model => model.Price >= priceLowerBound && model.Price <= priceUpperBound);
    }

    return TypedResults.Ok(await collection.ToListAsync());
}

static async Task<IResult> DeleteFilteredControllers(ServiceDbContext db,
    string? nameSearch = null, string? typeSearch = null, 
    int? releaseYearSearch = null, float priceLowerBound = 0, float priceUpperBound = float.MaxValue) {
    
    var collection = db.Controllers.AsQueryable();
    
    if (nameSearch != null) {
        collection = collection.Where(model => model.Name.Contains(nameSearch));
    }
    if (typeSearch != null) {
        collection = collection.Where(model => model.Type.Contains(typeSearch));
    }
    if (releaseYearSearch != null) {
        collection = collection.Where(model => model.Year == releaseYearSearch);
    }
    if (priceLowerBound != 0 || priceUpperBound < float.MaxValue) {
        collection = collection.Where(model => model.Price >= priceLowerBound && model.Price <= priceUpperBound);
    }

    var items = await collection.ToListAsync();
    if (items.Any()) {
        db.Controllers.RemoveRange(items);
    }
    await db.SaveChangesAsync();
    return TypedResults.NoContent();
}


    // Game
static async Task<IResult> GetFilteredGames(ServiceDbContext db,
    string? nameSearch = null, string? ageRestrictionSearch = null, 
    string? genreSearch = null, float priceLowerBound = 0, float priceUpperBound = float.MaxValue) {
    
    var collection = db.Games.AsQueryable();

    if (nameSearch != null) {
        collection = collection.Where(model => model.Name.Contains(nameSearch));
    }
    if (ageRestrictionSearch != null) {
        collection = collection.Where(model => model.AgeRestriction.Contains(ageRestrictionSearch));
    }
    if (genreSearch != null) {
        collection = collection.Where(model => model.Genre == genreSearch);
    }
    if (priceLowerBound != 0 || priceUpperBound < float.MaxValue) {
        collection = collection.Where(model => model.Price >= priceLowerBound && model.Price <= priceUpperBound);
    }

    return TypedResults.Ok(await collection.ToListAsync());
}

static async Task<IResult> DeleteFilteredGames(ServiceDbContext db,
    string? nameSearch = null, string? ageRestrictionSearch = null, 
    string? genreSearch = null, float priceLowerBound = 0, float priceUpperBound = float.MaxValue) {
    
    var collection = db.Games.AsQueryable();
    if (nameSearch != null) {
        collection = collection.Where(model => model.Name.Contains(nameSearch));
    }
    if (ageRestrictionSearch != null) {
        collection = collection.Where(model => model.AgeRestriction.Contains(ageRestrictionSearch));
    }
    if (genreSearch != null) {
        collection = collection.Where(model => model.Genre == genreSearch);
    }
    if (priceLowerBound != 0 || priceUpperBound < float.MaxValue) {
        collection = collection.Where(model => model.Price >= priceLowerBound && model.Price <= priceUpperBound);
    }

    var items = await collection.ToListAsync();
    if (items.Any()) {
        db.Games.RemoveRange(items);
    }
    await db.SaveChangesAsync();

    return TypedResults.NoContent();
}

//Business Models Complex Query Methods
    //DayStats
static async Task<IResult> GetFilteredAgenda(BusinessDbContext db, 
    DateTime? date = null,
    int totalConsumerLowerBound = 0, int totalConsumerUpperBound= int.MaxValue, 
    int totalProfitLowerBound = 0, int totalProfitUpperBound = int.MaxValue,
    int totalCostLowerBound = 0, int totalCostUpperBound = int.MaxValue) {

    var collection = db.Agenda.AsQueryable();

    if (date.HasValue) {
        collection = collection.Where(model => model.Day == date);        
    }
    if (totalConsumerLowerBound != 0 || totalConsumerUpperBound != int.MaxValue) {
        collection = collection.Where(model  => 
            (model.TotalConsumers >= totalConsumerLowerBound) && 
            (model.TotalConsumers <= totalConsumerUpperBound));
    }
    if (totalProfitLowerBound != 0 || totalProfitUpperBound != int.MaxValue) {
        collection = collection.Where(model  => 
            (model.TotalProfit >= totalProfitLowerBound) && 
            (model.TotalProfit <= totalProfitUpperBound));
    }
    if (totalCostLowerBound != 0 || totalCostUpperBound != int.MaxValue) {
        collection = collection.Where(model  => 
            (model.TotalCost >= totalCostLowerBound) && 
            (model.TotalCost <= totalCostUpperBound));
    }

    return TypedResults.Ok(await collection.ToListAsync());
}
    
static async Task<IResult> DeleteFilteredAgenda(BusinessDbContext db, 
    DateTime? date = null,
    int totalConsumerLowerBound = 0, int totalConsumerUpperBound= int.MaxValue, 
    int totalProfitLowerBound = 0, int totalProfitUpperBound = int.MaxValue,
    int totalCostLowerBound = 0, int totalCostUpperBound = int.MaxValue) {

    var collection = db.Agenda.AsQueryable();

    if (date.HasValue) {
        collection = collection.Where(model => model.Day == date);        
    }
    if (totalConsumerLowerBound != 0 || totalConsumerUpperBound != int.MaxValue) {
        collection = collection.Where(model  => 
            (model.TotalConsumers >= totalConsumerLowerBound) && 
            (model.TotalConsumers <= totalConsumerUpperBound));
    }
    if (totalProfitLowerBound != 0 || totalProfitUpperBound != int.MaxValue) {
        collection = collection.Where(model  => 
            (model.TotalProfit >= totalProfitLowerBound) && 
            (model.TotalProfit <= totalProfitUpperBound));
    }
    if (totalCostLowerBound != 0 || totalCostUpperBound != int.MaxValue) {
        collection = collection.Where(model  => 
            (model.TotalCost >= totalCostLowerBound) && 
            (model.TotalCost <= totalCostUpperBound));
    }

    var items = await collection.ToListAsync();
    if (items.Any()) {
        db.RemoveRange(items);
    }
    await db.SaveChangesAsync();

    return TypedResults.NoContent();
}
    
    //Employee

