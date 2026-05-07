using System;

namespace GimnasioApi.DTO.Cliente.AgregarTelefono;

public class AgregarTelefonoInput
{
    public required string Numero { get; set; }
    public int IdTipoTelefono { get; set; }
}
