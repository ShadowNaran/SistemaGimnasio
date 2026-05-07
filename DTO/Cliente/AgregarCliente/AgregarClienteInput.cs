using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;

namespace GimnasioApi.DTO.Cliente.AgregarCliente;

public class AgregarClienteInput
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener al menos 3 caracteres.")]
    public required string Nombre { get; set; } 
    [Required(ErrorMessage = "El CI es obligatorio.")]
    [Range(1, int.MaxValue, ErrorMessage = "El CI debe ser mayor a 0 y no puede ser negativo.")]
    public int CI { get; set; } 
    
}