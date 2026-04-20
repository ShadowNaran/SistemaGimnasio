namespace GimnasioApi.DTOs
{
    public class ClienteDTO
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; } = null!;
        public string CI { get; set; } = null!;
        public List<string> NumerosTelefonicos { get; set; } = new List<string>();
    }
}
