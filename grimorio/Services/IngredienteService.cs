using GrimorioDigital.DTOs;
using GrimorioDigital.Models;
using GrimorioDigital.Repositories;

namespace GrimorioDigital.Services;

public class IngredienteService : IIngredienteService
{
    private readonly IIngredienteRepository _repository;

    public IngredienteService(IIngredienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<IngredienteResponseDto>> GetAllAsync()
    {
        var ingredientes = await _repository.GetAllAsync();
        return ingredientes.Select(MapToResponse).ToList();
    }

    public async Task<IngredienteResponseDto?> GetByIdAsync(int id)
    {
        var ingrediente = await _repository.GetByIdAsync(id);
        return ingrediente == null ? null : MapToResponse(ingrediente);
    }

    public async Task<IngredienteResponseDto> CreateAsync(IngredienteCreateDto dto)
    {
        var ingrediente = new Ingrediente
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao,
            Raridade = dto.Raridade,
            Quantidade = dto.Quantidade
        };

        var createdIngrediente = await _repository.CreateAsync(ingrediente);
        return MapToResponse(createdIngrediente);
    }

    public async Task<IngredienteResponseDto?> UpdateAsync(int id, IngredienteUpdateDto dto)
    {
        var ingrediente = await _repository.GetByIdAsync(id);
        if (ingrediente == null)
            return null;

        ingrediente.Nome = dto.Nome;
        ingrediente.Descricao = dto.Descricao;
        ingrediente.Raridade = dto.Raridade;
        ingrediente.Quantidade = dto.Quantidade;

        var updatedIngrediente = await _repository.UpdateAsync(ingrediente);
        return MapToResponse(updatedIngrediente);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var ingrediente = await _repository.GetByIdAsync(id);
        if (ingrediente == null)
            return false;

        await _repository.DeleteAsync(id);
        return true;
    }

    private static IngredienteResponseDto MapToResponse(Ingrediente ingrediente)
    {
        return new IngredienteResponseDto
        {
            Id = ingrediente.Id,
            Nome = ingrediente.Nome,
            Descricao = ingrediente.Descricao,
            Raridade = ingrediente.Raridade,
            Quantidade = ingrediente.Quantidade
        };
    }
}
