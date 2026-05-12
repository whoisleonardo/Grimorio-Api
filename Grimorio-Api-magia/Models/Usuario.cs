namespace GrimorioDigital.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string SenhaHash { get; set; } = string.Empty;
    public string Role { get; set; } = "Membro"; // Admin | Membro

    // Navegação
    public ICollection<Feiticeiro> Feiticeiros { get; set; } = new List<Feiticeiro>();
}
