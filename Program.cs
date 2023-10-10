using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectoef;
using projectoef.Models;

// dotnet ef migrations add InitialCreate
//dotnet ef database update
// dotnet ef migrations add ColumnPesoCategoria

var builder = WebApplication.CreateBuilder(args);
// builder.Services.AddDbContext<TareasContext>(p => p.UseInMemoryDatabase("TareasDB"));
// builder.Services.AddSqlServer<TareasContext>(builder.Configuration.GetConnectionString("cnTareas"));
builder.Services.AddNpgsql<TareasContext>(builder.Configuration.GetConnectionString("cnTareasPostgres"));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);//Corregir problemas con timestamp

var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconexion", async ([FromServices] TareasContext dbContext) =>
{
    dbContext.Database.EnsureCreated();
    return Results.Ok("Base de datos en memoria: " + dbContext.Database.IsInMemory());
});

app.MapGet("/api/tareas", async ([FromServices] TareasContext dbContext) =>
{
    // return Results.Ok(dbContext.Tareas.Include(p => p.Categoria).Where(p => p.PrioridadTarea == projectoef.Models.Prioridad.Baja));
    return Results.Ok(dbContext.Tareas.Include(p => p.Categoria));
});

app.MapGet("/api/tareas/{id}", async ([FromServices] TareasContext dbContext, [FromRoute] Guid id) =>
{
    var tareas = dbContext.Tareas.Include(p=>p.Categoria);
    var mostrar = tareas.Where(p=>p.TareaId==id);
    if(mostrar!=null){
        return Results.Ok(mostrar);
    }
    return Results.Ok(dbContext.Tareas.Include(p => p.Categoria));
});

app.MapPost("/api/tareas", async ([FromServices] TareasContext dbContext, [FromBody] Tarea tarea) =>
{
    tarea.TareaId = Guid.NewGuid();
    tarea.FechaCreacion = DateTime.Now;
    await dbContext.Tareas.AddAsync(tarea);
    await dbContext.SaveChangesAsync();
    return Results.Ok();
});

app.MapPut("/api/tareas/{id}", async ([FromServices] TareasContext dbContext, [FromBody] Tarea tarea, [FromRoute] Guid id) =>
{
    var tareaActual = dbContext.Tareas.Find(id);
    if (tareaActual != null)
    {
        tareaActual.CategoriaId = tarea.CategoriaId;
        tareaActual.Titulo = tarea.Titulo;
        tareaActual.PrioridadTarea = tarea.PrioridadTarea;
        tareaActual.Descripcion=tarea.Descripcion;
        await dbContext.SaveChangesAsync();
        return Results.Ok();
    }
    else{
        return Results.NotFound("No se encontro elemento");
    }

});

app.MapDelete("/api/tareas/{id}", async ([FromServices] TareasContext dbContext, [FromRoute] Guid id) =>
{
   var tareaActual = dbContext.Tareas.Find(id);
    if(tareaActual!=null){
        dbContext.Tareas.Remove(tareaActual);
        await dbContext.SaveChangesAsync();
        return Results.Ok();
    }
    return Results.BadRequest();
});

app.Run();
