namespace GimnasioApi.DTO.Cliente.ActualizarCliente;

public class ActualizarClienteOutput
{
    public int IdCliente { get; set; }
    public required string Nombre { get; set; }
    public int CI { get; set; }
}