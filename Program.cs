using BrasGames.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Connecting to the Databases
builder.Services.AddDbContext<BusinessDbContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("BusinessDbConnection")));
builder.Services.AddDbContext<ServiceDbContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("ServiceDbConnection")));

if (builder.Environment.IsDevelopment()) {
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
}

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "BrasGames";
    config.Title = "BrasGames v1";
    config.Version = "v1";
});


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "BrasGames";
        //config.Path determines the path to access the swagger UI        
        config.Path = "/debugui";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}



app.MapGet("/", () => "I am the Sun God!!! I am untatered and my legion knows no bounds!!!");

app.Run("http://localhost:3000");
