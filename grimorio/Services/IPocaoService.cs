using GrimorioDigital.DTOs;

namespace GrimorioDigital.Services;

public interface IPocaoService
{
    Task<IEnumerable<PocaoResponseDto>> GetAllAsync();
    Task<PocaoResponseDto> GetByIdAsync(int id);
    Task<PocaoResponseDto> CreateAsync(PocaoCreateDto dto);
    Task<PocaoResponseDto> UpdateAsync(int id, PocaoUpdateDto dto);
    Task DeleteAsync(int id);
}
