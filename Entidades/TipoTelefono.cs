namespace GimnasioApi.Entidades;

public class TipoTelefono
{
    public int IdTipoTelefono { get; set; }
    public string Nombre { get; set; } = null!;

    public ICollection<Telefono> Telefonos { get; set; } = new List<Telefono>();
}