using BrasGames.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

/// !!! DO NOT FORGET TO TRY TO USE GENERIC TYPEDRESULT METHODS!!!
//Connecting to the Databases
builder.Services.AddDbContext<BusinessDbContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("BusinessDbConnection")));
builder.Services.AddDbContext<ServiceDbContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("ServiceDbConnection")));

if (builder.Environment.IsDevelopment()) {
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
}



var app = builder.Build();
if (app.Environment.IsDevelopment())
{
   
}

var service = app.MapGroup("/service");

var controller = service.MapGroup("/controller");

controller.MapGet("/", GetControllers);

controller.MapGet("/{id:int}", GetController);
controller.MapPost("/", PostController);

controller.MapPut("/{id}", PutController);

controller.MapDelete("/{id}", DeleteController);

controller.MapDelete("/", DeleteAllControllers);


var business = app.MapGroup("/business");



app.Run();

//Service classes

static async Task<IResult> GetControllers(ServiceDbContext db) {
    return TypedResults.Ok(await db.Controllers.ToListAsync());
}

static  async Task<IResult> GetController(int id, ServiceDbContext db) {
    var searchResult = await db.Controllers.FindAsync(id);
    if (searchResult is null)
        return TypedResults.NotFound();
    return TypedResults.Ok(searchResult);
}

static async Task<IResult> PostController(Controller userController, ServiceDbContext db) {
    await db.Controllers.AddAsync(userController);
    await db.SaveChangesAsync();
    return TypedResults.Created("/service/controller/" + userController.Id, userController);
}

static async Task<IResult> PutController(int id, Controller userController, ServiceDbContext db) {
    var result = await db.Controllers.FindAsync(id);
    if (result is null)
        return TypedResults.NotFound();

    result.Id = userController.Id;
    result.Type = userController.Type;
    result.Name = userController.Name;
    result.Year = userController.Year;
    result.Price = userController.Price;

    await db.SaveChangesAsync();

    return TypedResults.NotFound();
}

static async Task<IResult> DeleteController(int id, ServiceDbContext db) {
    var result = await db.Controllers.FindAsync(id);
    if (result is null)
        return TypedResults.NotFound();
    
    db.Controllers.Remove(result);
    await db.SaveChangesAsync();

    return TypedResults.NoContent();
}

static async Task<IResult> DeleteAllControllers(ServiceDbContext db) {
    var result = await db.Controllers.ToListAsync();
    if (result is null)
        return TypedResults.NotFound();
    
    foreach (var controller in result) {
        db.Controllers.Remove(controller);
    }
    await db.SaveChangesAsync();

    return TypedResults.NoContent();
}


//Business classes
