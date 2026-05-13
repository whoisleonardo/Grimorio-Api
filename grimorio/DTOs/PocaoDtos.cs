using  System.ComponentModel.DataAnnotations;

namespace GrimorioDigital.DTOs;

public class PocaoIngredienteDto
{ 
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Nome do ingrediente deve ter entre 2 e 100 caracteres")]
    public string NomeIngrediente { get; set; } = string.Empty;

    [Range(0, int.MaxValue, ErrorMessage = "IngredienteId deve ser 0 (novo) ou maior que zero (existente)")]
    public int IngredienteId { get; set; } = 0;

    [StringLength(20, ErrorMessage = "Raridade deve ter no máximo 20 caracteres")]
    public string Raridade { get; set; } = string.Empty;

    [Required(ErrorMessage = "QuantidadeNecessaria é obrigatória")]
    [Range(1, 999, ErrorMessage = "QuantidadeNecessaria deve ser entre 1 e 999")]
    public int QuantidadeNecessaria { get; set; }

    [StringLength(300, MinimumLength = 5, ErrorMessage = "Descricao deve ter entre 5 e 300 caracteres")]
    public string Descricao { get; set; } = string.Empty;
}


public class PocaoIngredienteResponseDto
{
    public IngredienteResponseDto Ingrediente { get; set; } = new();
    public int QuantidadeNecessaria { get; set; }
}


public class PocaoCreateDto
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Nome deve ter entre 2 e 100 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Efeito é obrigatório")]
    [StringLength(300, MinimumLength = 5, ErrorMessage = "Efeito deve ter entre 5 e 300 caracteres")]
    public string Efeito { get; set; } = string.Empty;

    [Required(ErrorMessage = "DuracaoMinutos é obrigatória")]
    [Range(1, 120, ErrorMessage = "DuracaoMinutos deve ser entre 1 e 120")]
    public int DuracaoMinutos { get; set; }

    public List<PocaoIngredienteDto> Ingredientes { get; set; } = new();
}

public class PocaoUpdateDto
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Nome deve ter entre 2 e 100 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Efeito é obrigatório")]
    [StringLength(300, MinimumLength = 5, ErrorMessage = "Efeito deve ter entre 5 e 300 caracteres")]
    public string Efeito { get; set; } = string.Empty;

    [Required(ErrorMessage = "DuracaoMinutos é obrigatória")]
    [Range(1, 120, ErrorMessage = "DuracaoMinutos deve ser entre 1 e 120")]
    public int DuracaoMinutos { get; set; }

    public List<PocaoIngredienteDto> Ingredientes { get; set; } = new();
}

public class PocaoResponseDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Efeito { get; set; } = string.Empty;
    public int DuracaoMinutos { get; set; }

    public List<PocaoIngredienteResponseDto> Ingredientes { get; set; } = new();
}