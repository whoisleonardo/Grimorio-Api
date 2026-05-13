using System.ComponentModel.DataAnnotations;

namespace GrimorioDigital.DTOs;

public class EscolaDeMagiaCreateDto
{
    [Required(ErrorMessage = "O nome da escola é obrigatório")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "A descrição é obrigatória")]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "A descrição deve ter entre 10 e 500 caracteres")]
    public string Descricao { get; set; } = string.Empty;

    [StringLength(50, MinimumLength = 3, ErrorMessage = "O elemento deve ter entre 3 e 50 caracteres")]
    public string Elemento { get; set; } = string.Empty;
}

public class EscolaDeMagiaUpdateDto
{
    [Required(ErrorMessage = "O nome da escola é obrigatório")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "A descrição é obrigatória")]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "A descrição deve ter entre 10 e 500 caracteres")]
    public string Descricao { get; set; } = string.Empty;

    [StringLength(50, MinimumLength = 3, ErrorMessage = "O elemento deve ter entre 3 e 50 caracteres")]
    public string Elemento { get; set; } = string.Empty;
}

public class EscolaDeMagiaResponseDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Elemento { get; set; } = string.Empty;
}
