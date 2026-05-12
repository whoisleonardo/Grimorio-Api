using System.ComponentModel.DataAnnotations;

namespace GrimorioDigital.DTOs;

public class FeiticeiroCreateDto
{
    [Required(ErrorMessage = "O nome do feiticeiro é obrigatório")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O nível mágico é obrigatório")]
    [Range(1, 100, ErrorMessage = "O nível mágico deve estar entre 1 e 100")]
    public int NivelMagico { get; set; }

    [Required(ErrorMessage = "A especialidade é obrigatória")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "A especialidade deve ter entre 3 e 100 caracteres")]
    public string Especialidade { get; set; } = string.Empty;

    [Required(ErrorMessage = "O ID da Escola de Magia é obrigatório")]
    [Range(1, int.MaxValue, ErrorMessage = "O ID da Escola de Magia deve ser maior que 0")]
    public int EscolaDeMagiaId { get; set; }

    [Required(ErrorMessage = "O ID do Usuário é obrigatório")]
    [Range(1, int.MaxValue, ErrorMessage = "O ID do Usuário deve ser maior que 0")]
    public int UsuarioId { get; set; }
}

public class FeiticeiroUpdateDto
{
    [Required(ErrorMessage = "O nome do feiticeiro é obrigatório")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O nível mágico é obrigatório")]
    [Range(1, 100, ErrorMessage = "O nível mágico deve estar entre 1 e 100")]
    public int NivelMagico { get; set; }

    [Required(ErrorMessage = "A especialidade é obrigatória")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "A especialidade deve ter entre 3 e 100 caracteres")]
    public string Especialidade { get; set; } = string.Empty;

    [Required(ErrorMessage = "O ID da Escola de Magia é obrigatório")]
    [Range(1, int.MaxValue, ErrorMessage = "O ID da Escola de Magia deve ser maior que 0")]
    public int EscolaDeMagiaId { get; set; }

    [Required(ErrorMessage = "O ID do Usuário é obrigatório")]
    [Range(1, int.MaxValue, ErrorMessage = "O ID do Usuário deve ser maior que 0")]
    public int UsuarioId { get; set; }
}

public class FeiticeiroResponseDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int NivelMagico { get; set; }
    public string Especialidade { get; set; } = string.Empty;
    public int EscolaDeMagiaId { get; set; }
    public int UsuarioId { get; set; }
    public EscolaDeMagiaResponseDto? EscolaDeMagia { get; set; }
    public UsuarioResponseDto? Usuario { get; set; }
}
