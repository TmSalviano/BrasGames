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

controller.MapGet("/", async (ServiceDbContext db) => {
    var result = await db.Controllers.ToListAsync();
    return Results.Ok(result);
});

controller.MapGet("/{id:int}", async (int id, ServiceDbContext db) => {
    var result = await db.Controllers.FindAsync(id);
    if (result is null)
        return Results.NotFound();
    return Results.Ok(result);
});
controller.MapPost("/", async (Controller controller, ServiceDbContext db) => {
    await db.Controllers.AddAsync(controller);
    await db.SaveChangesAsync();
    return Results.Created("/service/controller/" + controller.Id, controller);
});

controller.MapPut("/{id}", async  (int id, Controller inputController, ServiceDbContext db) => {
    var todo = await db.Controllers.FindAsync(id);

    if (todo is null) return Results.NotFound();

    todo.Name = inputController.Name;
    todo.Type = inputController.Type;
    todo.Price = inputController.Price;
    todo.Id = inputController.Id;
    todo.Year = inputController.Year;

    await db.SaveChangesAsync();
    
    return Results.NoContent();
});

controller.MapDelete("/{id}", async  (int id, ServiceDbContext db) => {
    if (await db.Controllers.FindAsync(id) is Controller controller)
    {
        db.Controllers.Remove(controller);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});


var business = app.MapGroup("/business");



app.Run();

//Service classes

//Business classes
