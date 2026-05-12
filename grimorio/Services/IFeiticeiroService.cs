using GrimorioDigital.DTOs;

namespace GrimorioDigital.Services;

public interface IFeiticeiroService
{
    Task<List<FeiticeiroResponseDto>> GetAllAsync();
    Task<FeiticeiroResponseDto?> GetByIdAsync(int id);
    Task<FeiticeiroResponseDto> CreateAsync(FeiticeiroCreateDto dto);
    Task<FeiticeiroResponseDto?> UpdateAsync(int id, FeiticeiroUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}
