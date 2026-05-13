using System.ComponentModel.DataAnnotations;

namespace GrimorioDigital.DTOs;

public class IngredienteCreateDto
{
    [Required(ErrorMessage = "O nome do ingrediente é obrigatório")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "A descrição é obrigatória")]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "A descrição deve ter entre 10 e 500 caracteres")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "A raridade é obrigatória")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "A raridade deve ter entre 2 e 50 caracteres")]
    public string Raridade { get; set; } = string.Empty;

    [Range(1, 10000, ErrorMessage = "A quantidade deve ser entre 1 e 10000")]
    public int Quantidade { get; set; }
}

public class IngredienteUpdateDto
{
    [Required(ErrorMessage = "O nome do ingrediente é obrigatório")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "A descrição é obrigatória")]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "A descrição deve ter entre 10 e 500 caracteres")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "A raridade é obrigatória")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "A raridade deve ter entre 2 e 50 caracteres")]
    public string Raridade { get; set; } = string.Empty;

    [Range(1, 10000, ErrorMessage = "A quantidade deve ser entre 1 e 10000")]
    public int Quantidade { get; set; }
}

public class IngredienteResponseDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Raridade { get; set; } = string.Empty;
    public int Quantidade { get; set; }
}
