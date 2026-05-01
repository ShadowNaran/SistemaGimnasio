namespace GimnasioApi.DTO.Plan.ListarPlanes;

public class PlanOutput
{
    public int IdPlan { get; set; }
    public required string Nombre { get; set; }
    public decimal Precio { get; set; }
    public required string Descripcion { get; set; }
    public bool Activo { get; set; }
}