namespace GimnasioApi.Entidades;
public class Beneficio
{
    public int IdBeneficio { get; set; }
    public int IdPlan { get; set; }
    public required string Descripcion { get; set; } 

    public required Plan Plan { get; set; } 
    /*
     * se separo en otra tabla porque un plan puede tener varios beneficios.
      si se guardaba todo en Plan se iba a llenar de columnas o datos repetidos.
     */
}