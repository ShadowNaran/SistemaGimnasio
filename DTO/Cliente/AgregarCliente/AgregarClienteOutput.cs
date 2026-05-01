namespace GimnasioApi.DTO.Cliente.AgregarCliente;

public class AgregarClienteOutput
{
    public int IdCliente { get; set; }
    public required string Nombre { get; set; } 
    public int CI { get; set; } 
}