using GrimorioDigital.DTOs;

namespace GrimorioDigital.Services;

public interface IIngredienteService
{
    Task<List<IngredienteResponseDto>> GetAllAsync();
    Task<IngredienteResponseDto?> GetByIdAsync(int id);
    Task<IngredienteResponseDto> CreateAsync(IngredienteCreateDto dto);
    Task<IngredienteResponseDto?> UpdateAsync(int id, IngredienteUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}
