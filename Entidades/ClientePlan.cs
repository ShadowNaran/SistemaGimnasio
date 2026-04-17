namespace GimnasioApi.Entidades;

public class ClientePlan
{
    public int IdClientePlan { get; set; }

    public int IdCliente { get; set; }
    public int IdPlan { get; set; }

    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public required string Estado { get; set; } 

    public required Cliente Cliente { get; set; } 
    public required Plan Plan { get; set; } 
}