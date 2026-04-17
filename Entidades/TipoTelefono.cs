namespace GimnasioApi.Entidades;

public class TipoTelefono
{
    public int IdTipoTelefono { get; set; }
    public required string Nombre { get; set; }

    public ICollection<Telefono> Telefonos { get; set; } = new List<Telefono>();
    /*
      esta tabla se creo para no guardar el tipo como texto en Telefono.
      Si se guardaba como string podían haber errores como:
      trabajo, Trabajo, trabjo
     
      asi se evita repetir datos y se mantiene más ordenado
      Averiguar si se puede poner en un enum
     */
}