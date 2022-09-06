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

app.MapGet("/tareas", async ([FromServices] TareasContext dbContext)=>
{
    return Results.Ok(dbContext.Tareas.Include(p=>p.Categoria).Where(p=>p.PrioridadTarea==proyef.Models.Prioridad.Baja));
});

app.MapPost("/tareas", async ([FromServices] TareasContext dbContext, [FromBody] proyef.Models.Tarea tarea)=>
{
    tarea.TareaId=Guid.NewGuid();
    tarea.FechaCreacion= DateTime.Now;
    await dbContext.AddAsync(tarea);
    //await dbContext.Tareas.AddAsync(tarea);
    
    await dbContext.SaveChangesAsync();
    return Results.Ok();
});

app.MapPut("/tareas/{id}", async ([FromServices] TareasContext dbContext, [FromBody] proyef.Models.Tarea tarea, [FromRoute] Guid id)=>
{
    var tareaActual= dbContext.Tareas.Find(id);

    if (tareaActual!=null)
    {
        tareaActual.CategoriaId=tarea.CategoriaId;
        tareaActual.Titulo= tarea.Titulo;
        tareaActual.PrioridadTarea=tarea.PrioridadTarea;
        tareaActual.Descripcion=tarea.Descripcion;
        await dbContext.SaveChangesAsync();
        return Results.Ok();
    }
    return Results.NotFound();
});

app.MapDelete("/tareas/{id}", async ([FromServices] TareasContext dbContext, [FromRoute] Guid id)=>
{
    var tareaActual= dbContext.Tareas.Find(id);

    if (tareaActual!=null)
    {
        dbContext.Remove(tareaActual);
        await dbContext.SaveChangesAsync();
        return Results.Ok();
    }
    return Results.NotFound();
});

app.Run();
