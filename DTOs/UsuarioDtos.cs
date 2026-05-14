using System.ComponentModel.DataAnnotations;

namespace GrimorioDigital.DTOs;

public class UsuarioCreateDto
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Nome deve ter entre 2 e 100 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    [StringLength(150, ErrorMessage = "Email deve ter no máximo 150 caracteres")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Senha é obrigatória")]
    [MinLength(6, ErrorMessage = "Senha deve ter no mínimo 6 caracteres")]
    [StringLength(100, ErrorMessage = "Senha deve ter no máximo 100 caracteres")]
    public string Senha { get; set; } = string.Empty;

    [StringLength(20, ErrorMessage = "Role inválida")]
    public string Role { get; set; } = "Membro";
}

public class UsuarioUpdateDto
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Nome deve ter entre 2 e 100 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    [StringLength(150, ErrorMessage = "Email deve ter no máximo 150 caracteres")]
    public string Email { get; set; } = string.Empty;

    [StringLength(20, ErrorMessage = "Role inválida")]
    public string Role { get; set; } = "Membro";
}

public class UsuarioResponseDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}
