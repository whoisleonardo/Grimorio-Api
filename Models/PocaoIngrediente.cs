namespace GrimorioDigital.Models;

public class PocaoIngrediente
{
    public int PocaoId { get; set; }
    public Pocao Pocao { get; set; } = null!;

    public int IngredienteId { get; set; }
    public Ingrediente Ingrediente { get; set; } = null!;

    public int QuantidadeNecessaria { get; set; }
}
