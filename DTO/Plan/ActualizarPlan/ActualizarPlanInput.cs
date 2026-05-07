using System;
using System.ComponentModel.DataAnnotations;

namespace GimnasioApi.DTO.Plan.ActualizarPlan;

public class ActualizarPlanInput
{
    [Required(ErrorMessage = "El nombre del plan es obligatorio.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener al menos 3 caracteres.")]
    public required string Nombre { get; set; }

    [Required(ErrorMessage = "El precio es obligatorio.")]
    [Range(0.01, 99999.99, ErrorMessage = "El precio debe ser mayor a 0.")]
    public decimal Precio { get; set; }

    [Required(ErrorMessage = "La descripcion es obligatoria.")]
    [StringLength(200, MinimumLength = 5, ErrorMessage = "Debe incluir una descripción valida (mínimo 5 caracteres).")]
    public required string Descripcion { get; set; }

   
    public bool Activo { get; set; }
}
