namespace GimnasioApi.DTO.Cliente.ObtenerCliente;

public class ObtenerClienteOutput
{
    public int IdCliente { get; set; }
    public required string Nombre { get; set; }
    public int CI { get; set; }
    public bool Activo { get; set; }
}