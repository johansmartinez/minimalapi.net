using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using proyef.Contexts;

var builder = WebApplication.CreateBuilder(args);
/* Bases de datos en Memoria
builder.Services.AddDbContext<TareasContext>(p=> p.UseInMemoryDatabase("TareasDB"));
*/

builder.Services.AddSqlServer<TareasContext>(builder.Configuration.GetConnectionString("cnTareas"));
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconexion", async ([FromServices] TareasContext dbContext)=>
{
    dbContext.Database.EnsureCreated();
    return Results.Ok("Base de datos en Memoria: " + dbContext.Database.IsInMemory());
});

app.Run();
