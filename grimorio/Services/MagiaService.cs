using GrimorioDigital.Data;
using GrimorioDigital.DTOs;
using GrimorioDigital.Models;
using Microsoft.EntityFrameworkCore;

namespace GrimorioDigital.Services;

public class MagiaService : IMagiaService
{
    private readonly AppDbContext _databaseContext;

    public MagiaService(AppDbContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<IEnumerable<MagiaResponseDto>> GetAllMagias()
    {
        var magias = await _databaseContext.Magias
            .Include(m => m.EscolaDeMagia)
            .Select(m => MapToResponseDto(m))
            .ToListAsync();

        return magias;
    }

    public async Task<MagiaResponseDto> GetMagiaById(int id)
    {
        var magia = await _databaseContext.Magias
            .Include(m => m.EscolaDeMagia)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (magia == null)
            throw new KeyNotFoundException("Magia não encontrada.");

        return MapToResponseDto(magia);
    }

    public async Task<MagiaResponseDto> CreateMagia(MagiaCreateDto dto)
    {
        var escolaExists = await _databaseContext.EscolasDeMagia
            .AnyAsync(e => e.Id == dto.EscolaDeMagiaId);

        if (!escolaExists)
            throw new ArgumentException("EscolaDeMagia não encontrada.");

        var magia = new Magia
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao,
            Nivel = dto.Nivel,
            TipoAlvo = dto.TipoAlvo,
            EscolaDeMagiaId = dto.EscolaDeMagiaId
        };

        _databaseContext.Magias.Add(magia);
        await _databaseContext.SaveChangesAsync();

        var createdMagia = await _databaseContext.Magias
            .Include(m => m.EscolaDeMagia)
            .FirstAsync(m => m.Id == magia.Id);

        return MapToResponseDto(createdMagia);
    }

    public async Task<MagiaResponseDto> UpdateMagia(int id, MagiaUpdateDto dto)
    {
        var magia = await _databaseContext.Magias.FindAsync(id);

        if (magia == null)
            throw new KeyNotFoundException("Magia não encontrada.");

        var escolaExists = await _databaseContext.EscolasDeMagia
            .AnyAsync(e => e.Id == dto.EscolaDeMagiaId);

        if (!escolaExists)
            throw new ArgumentException("EscolaDeMagia não encontrada.");

        magia.Nome = dto.Nome;
        magia.Descricao = dto.Descricao;
        magia.Nivel = dto.Nivel;
        magia.TipoAlvo = dto.TipoAlvo;
        magia.EscolaDeMagiaId = dto.EscolaDeMagiaId;

        _databaseContext.Magias.Update(magia);
        await _databaseContext.SaveChangesAsync();

        var updatedMagia = await _databaseContext.Magias
            .Include(m => m.EscolaDeMagia)
            .FirstAsync(m => m.Id == id);

        return MapToResponseDto(updatedMagia);
    }

    public async Task DeleteMagia(int id)
    {
        var magia = await _databaseContext.Magias.FindAsync(id);

        if (magia == null)
            throw new KeyNotFoundException("Magia não encontrada.");

        _databaseContext.Magias.Remove(magia);
        await _databaseContext.SaveChangesAsync();
    }

    private static MagiaResponseDto MapToResponseDto(Magia magia)
    {
        return new MagiaResponseDto
        {
            Id = magia.Id,
            Nome = magia.Nome,
            Descricao = magia.Descricao,
            Nivel = magia.Nivel,
            TipoAlvo = magia.TipoAlvo,
            EscolaDeMagiaId = magia.EscolaDeMagiaId,
            EscolaDeMagia = new EscolaDeMagiaResponseDto
            {
                Id = magia.EscolaDeMagia.Id,
                Nome = magia.EscolaDeMagia.Nome,
                Descricao = magia.EscolaDeMagia.Descricao,
                Elemento = magia.EscolaDeMagia.Elemento
            }
        };
    }
}
