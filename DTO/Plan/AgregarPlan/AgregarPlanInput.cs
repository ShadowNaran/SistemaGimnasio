using System;

namespace GimnasioApi.DTO.Plan.AgregarPlan;

public class AgregarPlanInput
{
    public required string Nombre { get; set; }
    public decimal Precio { get; set; }
    public required string Descripcion { get; set; }
}
