using System;
namespace GimnasioApi.DTO.Plan.AsignarPlan;


public class AsignarPlanesClienteInput
{
    public int IdCliente { get; set; }

    //  lista de planes (igual que "Detalle")
    public List<PlanEntrada> Planes { get; set; } = new();
}


public class PlanEntrada
{
    public int IdPlan { get; set; }
}