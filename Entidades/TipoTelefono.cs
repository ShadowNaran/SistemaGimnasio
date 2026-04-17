namespace GimnasioApi.Entidades;

public class TipoTelefono
{
    public int IdTipoTelefono { get; set; }
    public required string Nombre { get; set; }

    public ICollection<Telefono> Telefonos { get; set; } = new List<Telefono>();
}