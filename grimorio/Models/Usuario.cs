namespace GrimorioDigital.Models;

// Modelo de usuário do sistema - representa um feiticeiro registrado
public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string SenhaHash { get; set; } = string.Empty;  // Senha criptografada com BCrypt
    public string Role { get; set; } = "Membro"; // Níveis: Admin | Membro

    // Navegação
    public ICollection<Feiticeiro> Feiticeiros { get; set; } = new List<Feiticeiro>();
}
