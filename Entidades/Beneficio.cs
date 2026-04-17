namespace GimnasioApi.Entidades;
public class Beneficio
{
    public int IdBeneficio { get; set; }
    public int IdPlan { get; set; }
    public required string Descripcion { get; set; } 

    public required Plan Plan { get; set; } 
}