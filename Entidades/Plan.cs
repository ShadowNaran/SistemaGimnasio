namespace GimnasioApi.Entidades;

public class Plan
{
    public int IdPlan { get; set; }
    public required string Nombre { get; set; } 
    public decimal Precio { get; set; }
    public required string Descripcion { get; set; }

    public ICollection<Beneficio> Beneficios { get; set; } = new List<Beneficio>();
    public ICollection<ClientePlan> ClientesPlanes { get; set; } = new List<ClientePlan>();
    /*
      se agrego la descripción porque los planes no solo se diferencian por precio
      sino también por lo que ofrecen.
     */
}