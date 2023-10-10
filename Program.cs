using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectoef;

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
    return Results.Ok("Base de datos en memoria: "+dbContext.Database.IsInMemory());
});

app.Run();
