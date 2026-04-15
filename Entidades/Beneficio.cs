namespace GimnasioApi.Entidades;
public class Beneficio
{
    public int IdBeneficio { get; set; }
    public int IdPlan { get; set; }
    public string Descripcion { get; set; } = null!;

    public Plan Plan { get; set; } = null!;
}