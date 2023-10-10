using Microsoft.EntityFrameworkCore;
using projectoef.Models;

namespace proyectoef;


public class TareasContext : DbContext
{
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Tarea> Tareas { get; set; }
    public TareasContext(DbContextOptions<TareasContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        List<Categoria> categoriaInit = new List<Categoria>();
        categoriaInit.Add(new Categoria()
        {
            CategoriaId = Guid.Parse("5c317d28-3d67-4979-84e2-993be760edeb"),
            Nombre = "Actividades Pendientes",
            Peso = 220,
            Descripcion="Actividades importantes de hacer"
        });
        categoriaInit.Add(new Categoria()
        {
            CategoriaId = Guid.Parse("69dd5d86-01db-467e-8bc2-01c24f956aa5"),
            Nombre = "Actividades Personales",
            Peso = 50,
            Descripcion="Actividades que tienen que ver más con la persona y el oseo"
        });
        categoriaInit.Add(new Categoria()
        {
            CategoriaId = Guid.Parse("105dc87d-cbfa-4b09-875a-8c80e707a771"),
            Nombre = "A la madre",
            Peso = 22,
            Descripcion="Campo para saber que hay"
        });


        modelBuilder.Entity<Categoria>(categoria =>
        {
            categoria.ToTable("Categoria");
            categoria.HasKey(p => p.CategoriaId);
            categoria.Property(p => p.Nombre).IsRequired().HasMaxLength(150);
            categoria.Property(p => p.Descripcion).HasMaxLength(1000);
            categoria.Property(p => p.Peso);
            categoria.HasData(categoriaInit);
        });

        List<Tarea> tareasInit = new List<Tarea>();

        tareasInit.Add(new Tarea() { TareaId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb410"), CategoriaId = Guid.Parse("5c317d28-3d67-4979-84e2-993be760edeb"), PrioridadTarea = Prioridad.Media, Titulo = "Pago de servicios publicos", FechaCreacion = DateTime.Now });
        tareasInit.Add(new Tarea() { TareaId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb411"), CategoriaId = Guid.Parse("5c317d28-3d67-4979-84e2-993be760edeb"), PrioridadTarea = Prioridad.Baja, Titulo = "Terminar de ver pelicula en netflix", FechaCreacion = DateTime.Now });


        modelBuilder.Entity<Tarea>((tareas) =>
        {
            tareas.ToTable("Tareas");
            tareas.HasKey(p => p.TareaId);
            tareas.Property(p => p.Titulo).IsRequired().HasMaxLength(150);
            tareas.Property(p => p.Descripcion).HasMaxLength(1500).IsRequired(false);
            tareas.Property(p => p.PrioridadTarea);
            tareas.Property(p => p.FechaCreacion);
            tareas.HasOne(p => p.Categoria).WithMany(p => p.Tareas).HasForeignKey(p => p.CategoriaId);
            tareas.Ignore(p => p.Resumen);
            tareas.HasData(tareasInit);
        });
    }

}