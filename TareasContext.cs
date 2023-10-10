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
        modelBuilder.Entity<Categoria>(categoria =>
        {
            categoria.ToTable("Categoria");
            categoria.HasKey(p => p.CategoriaId);
            categoria.Property(p => p.Nombre).IsRequired().HasMaxLength(150);
            categoria.Property(p => p.Descripcion).HasMaxLength(1000);
        });
        modelBuilder.Entity<Tarea>((tareas) =>
        {
            tareas.ToTable("Tareas");
            tareas.HasKey(p => p.TareaId);
            tareas.Property(p => p.Titulo).IsRequired().HasMaxLength(150);
            tareas.Property(p => p.Descripcion).HasMaxLength(1500);
            tareas.Property(p => p.PrioridadTarea);
            tareas.Property(p => p.FechaCreacion);
            tareas.HasOne(p => p.Categoria).WithMany(p=>p.Tareas).HasForeignKey(p=>p.CategoriaId);
            tareas.Ignore(p=>p.Resumen);
        });
    }

}