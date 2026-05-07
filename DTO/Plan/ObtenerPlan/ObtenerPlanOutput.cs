using System;

namespace GimnasioApi.DTO.Plan.ObtenerPlan;

public class ObtenerPlanOutput
{
    public int IdPlan { get; set; }
    public required string Nombre { get; set; }
    public decimal Precio { get; set; }
    public required string Descripcion { get; set; }
    public bool Activo { get; set; }
}
