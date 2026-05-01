using System;

namespace GimnasioApi.DTO.Plan.AgregarPlan;

public class AgregarPlanOutput
{
        public int IdPlan { get; set; }
    public required string Nombre { get; set; } 
    public decimal Precio { get; set; }
    public required string Descripcion { get; set; } 
    public bool Activo { get; set; }
}
