namespace GrimorioDigital.Models;

public class EscolaDeMagia
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Elemento { get; set; } = string.Empty;

    // Navegação
    public ICollection<Magia> Magias { get; set; } = new List<Magia>();
    public ICollection<Feiticeiro> Feiticeiros { get; set; } = new List<Feiticeiro>();
}
