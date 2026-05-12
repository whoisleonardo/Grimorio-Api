using GrimorioDigital.DTOs;
using GrimorioDigital.Models;
using GrimorioDigital.Repositories;

namespace GrimorioDigital.Services;

public class EscolaDeMagiaService : IEscolaDeMagiaService
{
    private readonly IEscolaDeMagiaRepository _repository;

    public EscolaDeMagiaService(IEscolaDeMagiaRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<EscolaDeMagiaResponseDto>> GetAllAsync()
    {
        var escolas = await _repository.GetAllAsync();
        return escolas.Select(MapToResponse).ToList();
    }

    public async Task<EscolaDeMagiaResponseDto?> GetByIdAsync(int id)
    {
        var escola = await _repository.GetByIdAsync(id);
        return escola == null ? null : MapToResponse(escola);
    }

    public async Task<EscolaDeMagiaResponseDto> CreateAsync(EscolaDeMagiaCreateDto dto)
    {
        var escola = new EscolaDeMagia
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao,
            Elemento = dto.Elemento
        };

        var createdEscola = await _repository.CreateAsync(escola);
        return MapToResponse(createdEscola);
    }

    public async Task<EscolaDeMagiaResponseDto?> UpdateAsync(int id, EscolaDeMagiaUpdateDto dto)
    {
        var escola = await _repository.GetByIdAsync(id);
        if (escola == null)
            return null;

        escola.Nome = dto.Nome;
        escola.Descricao = dto.Descricao;
        escola.Elemento = dto.Elemento;

        var updatedEscola = await _repository.UpdateAsync(escola);
        return MapToResponse(updatedEscola);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var escola = await _repository.GetByIdAsync(id);
        if (escola == null)
            return false;

        await _repository.DeleteAsync(id);
        return true;
    }

    private static EscolaDeMagiaResponseDto MapToResponse(EscolaDeMagia escola)
    {
        return new EscolaDeMagiaResponseDto
        {
            Id = escola.Id,
            Nome = escola.Nome,
            Descricao = escola.Descricao,
            Elemento = escola.Elemento
        };
    }
}
