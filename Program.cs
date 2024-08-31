using BrasGames.Data;
using BrasGames.Model.ServiceModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using ConsoleModel = BrasGames.Model.ServiceModels.Console;

var builder = WebApplication.CreateBuilder(args);

// TypedResult Methods will be used for HTTP methods with complex LINQ query operations.
// Basic RESTApi operations should be done with user defined Services and registered in Program.cs
// Dont forget the DTO's 

//Connecting to the Databases
builder.Services.AddDbContext<BusinessDbContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("BusinessDbConnection")));
builder.Services.AddDbContext<ServiceDbContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("ServiceDbConnection")));
builder.Services.AddScoped(typeof(BasicRESTService<>));


if (builder.Environment.IsDevelopment()) {
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
}



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
   
}

var service = app.MapGroup("/service");

//Controller: HTTP Basic Methods
var controller = service.MapGroup("/controller");

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

//Game: HTTP Basic Methods
var game = service.MapGroup("/game");

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

//Order: HTTP Basic Methods
var order = service.MapGroup("/order");

order.MapGet("/", async (BasicRESTService<Order> basicRESTService) => {
    return await basicRESTService.GetAll();
});
order.MapGet("/{id:int}", async (int id, BasicRESTService<Order> basicRESTService) => {
    return await basicRESTService.GetId(id);
});
order.MapPost("/", async (Order order, BasicRESTService<Order> basicRESTService) => {
    return await basicRESTService.PostModel(order);
});
order.MapPut("/{id}", async (int id, Order order, BasicRESTService<Order> basicRESTService) => {
    return await basicRESTService.PutModel(id, order);
});
order.MapDelete("/{id}", async (int id, BasicRESTService<Order> basicRESTService) => {
    return await basicRESTService.DeleteModel(id);
});
order.MapDelete("/", async (BasicRESTService<Order> basicRESTService) => {
    return await basicRESTService.DeleteAllCModels();
});

//Console: HTTP Basic Methods. Using ConsoleModel as ServiceModels.Console
var console = service.MapGroup("/console");

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

var agenda = business.MapGroup("/agenda");



app.Run();

//Service classes

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


//Business classes
