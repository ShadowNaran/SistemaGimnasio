namespace GimnasioApi.DTO.Cliente.ObtenerCliente;

public class ObtenerClienteOutput
{
    public int IdCliente { get; set; }
    public required string Nombre { get; set; }
    public int CI { get; set; }
    public bool Activo { get; set; }
    
    // listar 
    public List<string> Telefonos { get; set; } = new List<string>();
    public List<string> PlanesActivos { get; set; } = new List<string>();
}