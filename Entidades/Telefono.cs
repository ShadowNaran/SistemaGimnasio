namespace GimnasioApi.Entidades;

public class Telefono
{
    public int IdTelefono { get; set; }
    public string Numero { get; set; } = null!;

    public int IdCliente { get; set; }
    public int IdTipoTelefono { get; set; } 

    public Cliente Cliente { get; set; } = null!;
    public TipoTelefono TipoTelefono { get; set; } = null!;
}