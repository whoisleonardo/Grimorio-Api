namespace GrimorioDigital.Models;

public class Feiticeiro
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Nivel { get; set; }

    // FK
    public int EscolaDeMagiaId { get; set; }
    public EscolaDeMagia EscolaDeMagia { get; set; } = null!;

    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;
}
