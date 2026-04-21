using System;
using System.Security.Authentication;

namespace GimnasioApi.DTO.Cliente.AgregarCliente;

public class AgregarClienteInput
{
    public required string Nombre { get; set; }
    public int CI { get; set; }
    
}