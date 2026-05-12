namespace GrimorioDigital.Models;

public class Ingrediente
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Raridade { get; set; } = string.Empty;
    public int Quantidade { get; set; }
    public ICollection<PocaoIngrediente> PocaoIngredientes { get; set; } = new List<PocaoIngrediente>();
}
