namespace GimnasioApi.Entidades;

public class Telefono
{
    public int IdTelefono { get; set; }
    public required string Numero { get; set; }

    public int IdCliente { get; set; }
    public int IdTipoTelefono { get; set; } 

    public required Cliente Cliente { get; set; }
    public required TipoTelefono TipoTelefono { get; set; }
}