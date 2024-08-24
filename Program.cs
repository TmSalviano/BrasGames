using BrasGames.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Connecting to the Databases
builder.Services.AddDbContext<BusinessDbContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("BusinessDbConnection")));
builder.Services.AddDbContext<ServiceDbContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("ServiceDbConnection")));

if (builder.Environment.IsDevelopment()) {
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
}

var app = builder.Build();



app.MapGet("/", () => "I am the Sun God!!! I am untatered and my legion knows no bounds!!!");

app.Run("http://localhost:3000");
