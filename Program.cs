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
    int? idLowerBound = null, int? idUpperBound = null,
    string? nameSearch = null, string? typeSearch = null, 
    DateTime? releaseYearSearch = null, float priceLowerBound = 0, float priceUpperBound = float.MaxValue) {

    var length = await db.Consoles.CountAsync();
    var collection = db.Consoles.AsQueryable();
    
        if (idLowerBound.HasValue || idUpperBound.HasValue) {
        if (idLowerBound.HasValue && (idLowerBound.Value < 0 || idLowerBound.Value >= length)) {
            return TypedResults.BadRequest("The provided lower bound ID is not valid.");
        }
        if (idUpperBound.HasValue && (idUpperBound.Value < 0 || idUpperBound.Value >= length)) {
            return TypedResults.BadRequest("The provided upper bound ID is not valid.");
        }
        if (idLowerBound.HasValue && idUpperBound.HasValue && idLowerBound.Value > idUpperBound.Value) {
            return TypedResults.BadRequest("The lower bound ID cannot be greater than the upper bound ID.");
        }

        collection = collection.Where(model => 
            (!idLowerBound.HasValue || model.Id >= idLowerBound.Value) &&
            (!idUpperBound.HasValue || model.Id <= idUpperBound.Value));
    }
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
    int? idLowerBound = null, int? idUpperBound = null,
    string? nameSearch = null, string? typeSearch = null, 
    DateTime? releaseYearSearch = null, float priceLowerBound = 0, float priceUpperBound = float.MaxValue) {
    
    var length = await db.Consoles.CountAsync();
    var collection = db.Consoles.AsQueryable();

    if (idLowerBound.HasValue || idUpperBound.HasValue) {
        if (idLowerBound.HasValue && (idLowerBound.Value < 0 || idLowerBound.Value >= length)) {
            return TypedResults.BadRequest("The provided lower bound ID is not valid.");
        }
        if (idUpperBound.HasValue && (idUpperBound.Value < 0 || idUpperBound.Value >= length)) {
            return TypedResults.BadRequest("The provided upper bound ID is not valid.");
        }
        if (idLowerBound.HasValue && idUpperBound.HasValue && idLowerBound.Value > idUpperBound.Value) {
            return TypedResults.BadRequest("The lower bound ID cannot be greater than the upper bound ID.");
        }

        collection = collection.Where(model => 
            (!idLowerBound.HasValue || model.Id >= idLowerBound.Value) &&
            (!idUpperBound.HasValue || model.Id <= idUpperBound.Value));
    }
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

    // Controller
static async Task<IResult> GetFilteredControllers(ServiceDbContext db,
    int? idLowerBound = null, int? idUpperBound = null,
    string? nameSearch = null, string? typeSearch = null, 
    int? releaseYearSearch = null, float priceLowerBound = 0, float priceUpperBound = float.MaxValue) {
    
    var length = await db.Controllers.CountAsync();
    var collection = db.Controllers.AsQueryable();

    if (idLowerBound.HasValue || idUpperBound.HasValue) {
        if (idLowerBound.HasValue && (idLowerBound.Value < 0 || idLowerBound.Value >= length)) {
            return TypedResults.BadRequest("The provided lower bound ID is not valid.");
        }
        if (idUpperBound.HasValue && (idUpperBound.Value < 0 || idUpperBound.Value >= length)) {
            return TypedResults.BadRequest("The provided upper bound ID is not valid.");
        }
        if (idLowerBound.HasValue && idUpperBound.HasValue && idLowerBound.Value > idUpperBound.Value) {
            return TypedResults.BadRequest("The lower bound ID cannot be greater than the upper bound ID.");
        }

        collection = collection.Where(model => 
            (!idLowerBound.HasValue || model.Id >= idLowerBound.Value) &&
            (!idUpperBound.HasValue || model.Id <= idUpperBound.Value));
    }
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
    int? idLowerBound = null, int? idUpperBound = null,
    string? nameSearch = null, string? typeSearch = null, 
    int? releaseYearSearch = null, float priceLowerBound = 0, float priceUpperBound = float.MaxValue) {
    
    var length = await db.Controllers.CountAsync();
    var collection = db.Controllers.AsQueryable();

    if (idLowerBound.HasValue || idUpperBound.HasValue) {
        if (idLowerBound.HasValue && (idLowerBound.Value < 0 || idLowerBound.Value >= length)) {
            return TypedResults.BadRequest("The provided lower bound ID is not valid.");
        }
        if (idUpperBound.HasValue && (idUpperBound.Value < 0 || idUpperBound.Value >= length)) {
            return TypedResults.BadRequest("The provided upper bound ID is not valid.");
        }
        if (idLowerBound.HasValue && idUpperBound.HasValue && idLowerBound.Value > idUpperBound.Value) {
            return TypedResults.BadRequest("The lower bound ID cannot be greater than the upper bound ID.");
        }

        collection = collection.Where(model => 
            (!idLowerBound.HasValue || model.Id >= idLowerBound.Value) &&
            (!idUpperBound.HasValue || model.Id <= idUpperBound.Value));
    }
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

    db.RemoveRange(await collection.ToListAsync());
    await db.SaveChangesAsync();
    return TypedResults.NoContent();
}


    // Game
static async Task<IResult> GetFilteredGames(ServiceDbContext db,
    int? idLowerBound = null, int? idUpperBound = null,
    string? nameSearch = null, string? ageRestrictionSearch = null, 
    string? genreSearch = null, float priceLowerBound = 0, float priceUpperBound = float.MaxValue) {
    
    var length = await db.Games.CountAsync();
    var collection = db.Games.AsQueryable();

    if (idLowerBound.HasValue || idUpperBound.HasValue) {
        if (idLowerBound.HasValue && (idLowerBound.Value < 0 || idLowerBound.Value >= length)) {
            return TypedResults.BadRequest("The provided lower bound ID is not valid.");
        }
        if (idUpperBound.HasValue && (idUpperBound.Value < 0 || idUpperBound.Value >= length)) {
            return TypedResults.BadRequest("The provided upper bound ID is not valid.");
        }
        if (idLowerBound.HasValue && idUpperBound.HasValue && idLowerBound.Value > idUpperBound.Value) {
            return TypedResults.BadRequest("The lower bound ID cannot be greater than the upper bound ID.");
        }

        collection = collection.Where(model => 
            (!idLowerBound.HasValue || model.Id >= idLowerBound.Value) &&
            (!idUpperBound.HasValue || model.Id <= idUpperBound.Value));
    }
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
    int? idLowerBound = null, int? idUpperBound = null,
    string? nameSearch = null, string? ageRestrictionSearch = null, 
    string? genreSearch = null, float priceLowerBound = 0, float priceUpperBound = float.MaxValue) {
    
    var length = await db.Games.CountAsync();
    var collection = db.Games.AsQueryable();

    if (idLowerBound.HasValue || idUpperBound.HasValue) {
        if (idLowerBound.HasValue && (idLowerBound.Value < 0 || idLowerBound.Value >= length)) {
            return TypedResults.BadRequest("The provided lower bound ID is not valid.");
        }
        if (idUpperBound.HasValue && (idUpperBound.Value < 0 || idUpperBound.Value >= length)) {
            return TypedResults.BadRequest("The provided upper bound ID is not valid.");
        }
        if (idLowerBound.HasValue && idUpperBound.HasValue && idLowerBound.Value > idUpperBound.Value) {
            return TypedResults.BadRequest("The lower bound ID cannot be greater than the upper bound ID.");
        }

        collection = collection.Where(model => 
            (!idLowerBound.HasValue || model.Id >= idLowerBound.Value) &&
            (!idUpperBound.HasValue || model.Id <= idUpperBound.Value));
    }
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

    db.Games.RemoveRange(collection);
    await db.SaveChangesAsync();

    return TypedResults.NoContent();
}


// static async Task<IResult> GetControllers(ServiceDbContext db) {
//     return TypedResults.Ok(await db.Controllers.ToListAsync());
// }

// static  async Task<IResult> GetController(int id, ServiceDbContext db) {
//     var searchResult = await db.Controllers.FindAsync(id);
//     if (searchResult is null)
//         return TypedResults.NotFound();
//     return TypedResults.Ok(searchResult);
// }

// static async Task<IResult> PostController(Controller userController, ServiceDbContext db) {
//     await db.Controllers.AddAsync(userController);
//     await db.SaveChangesAsync();
//     return TypedResults.Created("/service/controller/" + userController.Id, userController);
// }

// static async Task<IResult> PutController(int id, Controller userController, ServiceDbContext db) {
//     var result = await db.Controllers.FindAsync(id);
//     if (result is null)
//         return TypedResults.NotFound();

//     result.Id = userController.Id;
//     result.Type = userController.Type;
//     result.Name = userController.Name;
//     result.Year = userController.Year;
//     result.Price = userController.Price;

//     await db.SaveChangesAsync();

//     return TypedResults.NoContent();
// }

// static async Task<IResult> DeleteController(int id, ServiceDbContext db) {
//     var result = await db.Controllers.FindAsync(id);
//     if (result is null)
//         return TypedResults.NotFound();
    
//     db.Controllers.Remove(result);
//     await db.SaveChangesAsync();

//     return TypedResults.NoContent();
// }

// static async Task<IResult> DeleteAllControllers(ServiceDbContext db) {
//     var result = await db.Controllers.ToListAsync();
//     if (result is null)
//         return TypedResults.NotFound();
    
//     foreach (var controller in result) {
//         db.Controllers.Remove(controller);
//     }
//     await db.SaveChangesAsync();

//     return TypedResults.NoContent();
// }


//Business Models Complex Query Methods
    //DayStats
    //Employee

