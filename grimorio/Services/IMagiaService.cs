using GrimorioDigital.DTOs;

namespace GrimorioDigital.Services;

public interface IMagiaService
{
    Task<IEnumerable<MagiaResponseDto>> GetAllMagias();
    Task<MagiaResponseDto> GetMagiaById(int id);
    Task<MagiaResponseDto> CreateMagia(MagiaCreateDto dto);
    Task<MagiaResponseDto> UpdateMagia(int id, MagiaUpdateDto dto);
    Task DeleteMagia(int id);
}
