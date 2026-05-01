using System;

namespace GimnasioApi.DTO.Cliente.ListarClientes;

public class ListarClientesOutput
{
    public int IdCliente { get; set; }
    public required string Nombre { get; set; } 
    public int CI { get; set; } 
    public bool Activo { get; set; }
}