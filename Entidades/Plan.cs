namespace GimnasioApi.Entidades;

public class Plan
{
    public int IdPlan { get; set; }
    public string Nombre { get; set; } = null!;
    public decimal Precio { get; set; }
    public string Descripcion { get; set; } = null!;

    public ICollection<Beneficio> Beneficios { get; set; } = new List<Beneficio>();
    public ICollection<ClientePlan> ClientesPlanes { get; set; } = new List<ClientePlan>();
}