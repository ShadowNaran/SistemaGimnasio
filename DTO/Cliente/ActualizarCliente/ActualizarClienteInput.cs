using System.ComponentModel.DataAnnotations;

namespace GimnasioApi.DTO.Cliente.ActualizarCliente;

public class ActualizarClienteInput

{
    
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener al menos 3 caracteres.")]
    public required string Nombre { get; set; } 
    [Required(ErrorMessage = "El CI es obligatorio.")]
    [Range(1, int.MaxValue, ErrorMessage = "El CI debe ser mayor a 0 y no puede ser negativo.")]
    public int CI { get; set; } 

    public bool Activo { get; set; }
}