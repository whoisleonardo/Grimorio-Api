using System.ComponentModel.DataAnnotations;

namespace GrimorioDigital.DTOs;

public class MagiaCreateDto
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Nome deve ter entre 2 e 100 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Descrição é obrigatória")]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "Descrição deve ter entre 10 e 500 caracteres")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "Nível é obrigatório")]
    [Range(1, 10, ErrorMessage = "Nível deve estar entre 1 e 10")]
    public int Nivel { get; set; }

    [Required(ErrorMessage = "Tipo de Alvo é obrigatório")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Tipo de Alvo deve ter entre 2 e 50 caracteres")]
    public string TipoAlvo { get; set; } = string.Empty;

    [Required(ErrorMessage = "ID da Escola de Magia é obrigatório")]
    [Range(1, int.MaxValue, ErrorMessage = "ID da Escola de Magia deve ser maior que 0")]
    public int EscolaDeMagiaId { get; set; }
}

public class MagiaUpdateDto
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Nome deve ter entre 2 e 100 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Descrição é obrigatória")]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "Descrição deve ter entre 10 e 500 caracteres")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "Nível é obrigatório")]
    [Range(1, 10, ErrorMessage = "Nível deve estar entre 1 e 10")]
    public int Nivel { get; set; }

    [Required(ErrorMessage = "Tipo de Alvo é obrigatório")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Tipo de Alvo deve ter entre 2 e 50 caracteres")]
    public string TipoAlvo { get; set; } = string.Empty;

    [Required(ErrorMessage = "ID da Escola de Magia é obrigatório")]
    [Range(1, int.MaxValue, ErrorMessage = "ID da Escola de Magia deve ser maior que 0")]
    public int EscolaDeMagiaId { get; set; }
}

public class MagiaResponseDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public int Nivel { get; set; }
    public string TipoAlvo { get; set; } = string.Empty;
    public int EscolaDeMagiaId { get; set; }
    public EscolaDeMagiaResponseDto? EscolaDeMagia { get; set; }
}

public class EscolaDeMagiaResponseDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Elemento { get; set; } = string.Empty;
}
