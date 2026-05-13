namespace GrimorioDigital.Models;

public class Pocao
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Efeito { get; set; } = string.Empty;
    public int DuracaoMinutos { get; set; }
    public ICollection<PocaoIngrediente> PocaoIngredientes { get; set; } = new List<PocaoIngrediente>();
}
