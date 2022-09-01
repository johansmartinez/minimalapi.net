namespace proyef.Models;
using System.ComponentModel.DataAnnotations;

public class Categoria
{
    //[Key] //para llaves primarias
    public Guid CategoriaId {get;set;}

    //[Required] //para hacer un campo obligatorio
    //[MaxLength(150)] //restringir el tamaño
    public string Nombre {get;set;}

    public string Descripcion {get;set;}

    public ICollection<Tarea> Tareas {get;set;}
}