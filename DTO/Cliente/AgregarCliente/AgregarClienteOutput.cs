using System;

namespace GimnasioApi.DTO.Cliente.AgregarCliente;

public class AgregarClienteOutput
{
    public required string Nombre { get; set; }
    public int CI { get; set; }
}