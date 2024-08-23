var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "I am the Sun God!!! I am untatered and my legion knows no bounds!!!");


app.Run();
