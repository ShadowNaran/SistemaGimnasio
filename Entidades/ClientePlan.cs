namespace GimnasioApi.Entidades;

public class ClientePlan
{
    public int IdClientePlan { get; set; }

    public int IdCliente { get; set; }
    public int IdPlan { get; set; }

    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public string Estado { get; set; } = null!; 

    public Cliente Cliente { get; set; } = null!;
    public Plan Plan { get; set; } = null!;
}