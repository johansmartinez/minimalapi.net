namespace proyef.Models;
using System.Text.Json.Serialization;

public class Categoria
{
    //[Key] //para llaves primarias
    public Guid CategoriaId {get;set;}

    //[Required] //para hacer un campo obligatorio
    //[MaxLength(150)] //restringir el tamaño
    public string Nombre {get;set;}

    public string Descripcion {get;set;}

    public int Peso {get;set;}

    [JsonIgnore]
    public ICollection<Tarea> Tareas {get;set;}
}