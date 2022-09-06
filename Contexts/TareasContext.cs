namespace proyef.Contexts;

using proyef.Models;
using Microsoft.EntityFrameworkCore;
public class TareasContext: DbContext
{
    public DbSet<Categoria> Categorias {get;set;}
    public DbSet<Tarea> Tareas {get;set;}

    public TareasContext(DbContextOptions<TareasContext> options) :base (options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //creando datos semilla
        List<Categoria> categoriasInit=new List<Categoria>();
        categoriasInit.Add(new Categoria(){CategoriaId=Guid.Parse("20c1bb98-23fc-43b1-8c31-a62083ec0d8b"), Nombre="hola", Peso=20});
        categoriasInit.Add(new Categoria(){CategoriaId=Guid.Parse("f36b02e7-0654-4797-812a-73d2ecc3ae6c"), Nombre="Personales", Peso=50});

        modelBuilder.Entity<Categoria>(categoria=>
        {
            categoria.ToTable("Categoria");
            categoria.HasKey(p=>p.CategoriaId);

            categoria.Property(p=> p.Nombre).IsRequired().HasMaxLength(150);

            categoria.Property(p=>p.Descripcion).IsRequired(false);
            categoria.Property(p=>p.Peso);

            categoria.HasData(categoriasInit); //agregando los datos iniciales
        });

        modelBuilder.Entity<Tarea>(tarea=>
        {
            List<Tarea> tareasInit=new List<Tarea>();
            tareasInit.Add(new Tarea(){TareaId=Guid.Parse("8b0bc293-e80d-43b5-b7f9-553c76e57de4"), CategoriaId=Guid.Parse("20c1bb98-23fc-43b1-8c31-a62083ec0d8b"), PrioridadTarea=Prioridad.Media, Titulo="resisar algo", FechaCreacion=DateTime.Now});
            tareasInit.Add(new Tarea(){TareaId=Guid.Parse("ff97f1d1-0a63-458a-a96b-3d92f1eef40c"), CategoriaId=Guid.Parse("f36b02e7-0654-4797-812a-73d2ecc3ae6c"), PrioridadTarea=Prioridad.Baja, Titulo="terminar algo", FechaCreacion=DateTime.Now});
            tarea.ToTable("Tarea");
            tarea.HasKey(p=>p.TareaId);
            
            tarea.HasOne(p=>p.Categoria).WithMany(p=>p.Tareas).HasForeignKey(p=>p.CategoriaId);

            tarea.Property(p=>p.Titulo).IsRequired().HasMaxLength(200);

            tarea.Property(p=>p.Descripcion).IsRequired(false);
            tarea.Property(p=>p.PrioridadTarea);
            tarea.Property(p=>p.FechaCreacion);

            tarea.Ignore(p=>p.Resumen);

            tarea.HasData(tareasInit);
        });
    }
}