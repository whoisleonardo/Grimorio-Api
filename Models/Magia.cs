namespace GrimorioDigital.Models;

public class Magia
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public int Nivel { get; set; }
    public string TipoAlvo { get; set; } = string.Empty; // Individual | Area | Aliado

    // FK
    public int EscolaDeMagiaId { get; set; }
    public EscolaDeMagia EscolaDeMagia { get; set; } = null!;
}
