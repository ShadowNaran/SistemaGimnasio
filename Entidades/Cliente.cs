namespace GimnasioApi.Entidades;

public class Cliente {
    public int IdCliente { get; set; }
    public required string Nombre { get; set; }
    public required string CI { get; set; }
    public ICollection<Telefono> Telefonos { get; set;} = new List<Telefono>();

    public ICollection<ClientePlan> ClientesPlanes { get; set; } = new List<ClientePlan>();
    /*
      Se separo teléfono en otra tabla porque un cliente puede tener mas de uno
     */
}