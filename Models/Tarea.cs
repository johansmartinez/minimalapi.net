namespace proyef.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Tarea
{
    //[Key]
    public Guid TareaId {get;set;}
    //[ForeignKey("CategoriaId")]//laves foraneas
    public Guid CategoriaId {get;set;}
    //[Required]
    //[MaxLength(200)]
    public string Titulo {get;set;}

    public string Descripcion {get;set;}
    public Prioridad PrioridadTarea {get;set;}
    public DateTime FechaCreacion {get;set;}

    public virtual Categoria Categoria {get;set;}

    //[NotMapped] //no lo agrega a la base de datos
    public string Resumen {get;set;}
}

public enum Prioridad
{
    Baja,
    Media,
    Alta
}