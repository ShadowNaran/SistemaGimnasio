namespace GimnasioApi.Entidades;

public class Plan
{
    public int IdPlan { get; set; }
    public required string Nombre { get; set; } 
    public decimal Precio { get; set; }
    public required string Descripcion { get; set; }

    public bool Activo { get; set; } = true;

    public ICollection<Beneficio> Beneficios { get; set; } = new List<Beneficio>();
    public ICollection<ClientePlan> ClientesPlanes { get; set; } = new List<ClientePlan>();
    /*
      se agrego la descripcion porque los planes no solo se diferencian por precio
      sino tambien por lo que ofrecen
     */
}