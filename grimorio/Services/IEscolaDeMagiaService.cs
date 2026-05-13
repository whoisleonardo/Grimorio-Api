using GrimorioDigital.DTOs;

namespace GrimorioDigital.Services;

public interface IEscolaDeMagiaService
{
    Task<List<EscolaDeMagiaResponseDto>> GetAllAsync();
    Task<EscolaDeMagiaResponseDto?> GetByIdAsync(int id);
    Task<EscolaDeMagiaResponseDto> CreateAsync(EscolaDeMagiaCreateDto dto);
    Task<EscolaDeMagiaResponseDto?> UpdateAsync(int id, EscolaDeMagiaUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}
