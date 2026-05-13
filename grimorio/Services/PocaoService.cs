using GrimorioDigital.Data;
using GrimorioDigital.DTOs;
using GrimorioDigital.Models;
using GrimorioDigital.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GrimorioDigital.Services;

public class PocaoService : IPocaoService
{
    private readonly IPocaoRepository _pocaoRepository;
    private readonly IIngredienteRepository _ingredienteRepository;
    private readonly AppDbContext _context;

    public PocaoService(IPocaoRepository pocaoRepository, IIngredienteRepository ingredienteRepository, AppDbContext context)
    {
        _pocaoRepository = pocaoRepository;
        _ingredienteRepository = ingredienteRepository;
        _context = context;
    }

    public async Task<IEnumerable<PocaoResponseDto>> GetAllAsync()
    {
        var pocoes = await _pocaoRepository.GetAllAsync();
        return pocoes.Select(MapToResponseDto);
    }

    public async Task<PocaoResponseDto> GetByIdAsync(int id)
    {
        var pocao = await _pocaoRepository.GetByIdAsync(id);

        if (pocao == null)
            throw new KeyNotFoundException("Poção não encontrada.");

        return MapToResponseDto(pocao);
    }

    public async Task<PocaoResponseDto> CreateAsync(PocaoCreateDto dto)
    {
        if (dto.Ingredientes == null || dto.Ingredientes.Count == 0)
            throw new ArgumentException("A poção deve ter pelo menos 1 ingrediente.");

        var idsNoPayload = dto.Ingredientes
            .Where(i => i.IngredienteId > 0)
            .Select(i => i.IngredienteId)
            .ToList();

        if (idsNoPayload.Count != idsNoPayload.Distinct().Count())
            throw new ArgumentException("Há ingredientes duplicados na lista.");

        var idsExistentes = await _pocaoRepository.GetExistingIngredientIdsAsync(idsNoPayload);

        var idsInvalidos = idsNoPayload.Except(idsExistentes).ToList();
        if (idsInvalidos.Any())
            throw new KeyNotFoundException($"Ingredientes não encontrados: {string.Join(", ", idsInvalidos)}");

        var pocao = new Pocao
        {
            Nome = dto.Nome,
            Efeito = dto.Efeito,
            DuracaoMinutos = dto.DuracaoMinutos
        };

        var pocaoCriada = await _pocaoRepository.CreateAsync(pocao);

        foreach (var ingredienteDto in dto.Ingredientes.Where(i => i.IngredienteId == 0))
        {
            var novoIngrediente = new Ingrediente
            {
                Nome = ingredienteDto.NomeIngrediente,
                Raridade = ingredienteDto.Raridade
            };
            await _ingredienteRepository.CreateAsync(novoIngrediente);
        }

        foreach (var ingredienteDto in dto.Ingredientes)
        {
            int ingredienteId = ingredienteDto.IngredienteId;

            if (ingredienteId == 0)
            {
                var ingrediente = await _context.Ingredientes
                    .FirstOrDefaultAsync(i => i.Nome == ingredienteDto.NomeIngrediente);
                if (ingrediente != null)
                    ingredienteId = ingrediente.Id;
            }

            var pocaoIngrediente = new PocaoIngrediente
            {
                PocaoId = pocaoCriada.Id,
                IngredienteId = ingredienteId,
                QuantidadeNecessaria = ingredienteDto.QuantidadeNecessaria
            };
            _context.PocaoIngredientes.Add(pocaoIngrediente);
        }

        await _context.SaveChangesAsync();

        var pocaoComIngredientes = await _pocaoRepository.GetByIdAsync(pocaoCriada.Id);

        if (pocaoComIngredientes == null)
            throw new InvalidOperationException("Erro ao criar a poção.");

        return MapToResponseDto(pocaoComIngredientes);
    }

    public async Task<PocaoResponseDto> UpdateAsync(int id, PocaoUpdateDto dto)
    {
        if (dto.Ingredientes == null || dto.Ingredientes.Count == 0)
            throw new ArgumentException("A poção deve ter pelo menos 1 ingrediente.");

        var idsNoPayload = dto.Ingredientes
            .Where(i => i.IngredienteId > 0)
            .Select(i => i.IngredienteId)
            .ToList();

        if (idsNoPayload.Count != idsNoPayload.Distinct().Count())
            throw new ArgumentException("Há ingredientes duplicados na lista.");

        var pocao = await _pocaoRepository.GetByIdAsync(id);

        if (pocao == null)
            throw new KeyNotFoundException("Poção não encontrada.");

        var idsExistentes = await _pocaoRepository.GetExistingIngredientIdsAsync(idsNoPayload);

        var idsInvalidos = idsNoPayload.Except(idsExistentes).ToList();
        if (idsInvalidos.Any())
            throw new KeyNotFoundException($"Ingredientes não encontrados: {string.Join(", ", idsInvalidos)}");

        pocao.Nome = dto.Nome;
        pocao.Efeito = dto.Efeito;
        pocao.DuracaoMinutos = dto.DuracaoMinutos;

        _context.PocaoIngredientes.RemoveRange(pocao.PocaoIngredientes);

        foreach (var ingredienteDto in dto.Ingredientes.Where(i => i.IngredienteId == 0))
        {
            var novoIngrediente = new Ingrediente
            {
                Nome = ingredienteDto.NomeIngrediente,
                Raridade = ingredienteDto.Raridade
            };
            await _ingredienteRepository.CreateAsync(novoIngrediente);
        }

        foreach (var ingredienteDto in dto.Ingredientes)
        {
            int ingredienteId = ingredienteDto.IngredienteId;

            if (ingredienteId == 0)
            {
                var ingrediente = await _context.Ingredientes
                    .FirstOrDefaultAsync(i => i.Nome == ingredienteDto.NomeIngrediente);
                if (ingrediente != null)
                    ingredienteId = ingrediente.Id;
            }

            var pocaoIngrediente = new PocaoIngrediente
            {
                PocaoId = pocao.Id,
                IngredienteId = ingredienteId,
                QuantidadeNecessaria = ingredienteDto.QuantidadeNecessaria
            };
            _context.PocaoIngredientes.Add(pocaoIngrediente);
        }

        await _context.SaveChangesAsync();

        await _pocaoRepository.UpdateAsync(pocao);

        var pocaoAtualizada = await _pocaoRepository.GetByIdAsync(id);

        if (pocaoAtualizada == null)
            throw new InvalidOperationException("Erro ao atualizar a poção.");

        return MapToResponseDto(pocaoAtualizada);
    }

    public async Task DeleteAsync(int id)
    {
        var pocao = await _pocaoRepository.GetByIdAsync(id);

        if (pocao == null)
            throw new KeyNotFoundException("Poção não encontrada.");

        await _pocaoRepository.DeleteAsync(id);
    }

    private static PocaoResponseDto MapToResponseDto(Pocao pocao)
    {
        return new PocaoResponseDto
        {
            Id = pocao.Id,
            Nome = pocao.Nome,
            Efeito = pocao.Efeito,
            DuracaoMinutos = pocao.DuracaoMinutos,
            Ingredientes = pocao.PocaoIngredientes
                .Where(pi => pi.Ingrediente != null)
                .Select(pi => new PocaoIngredienteResponseDto
                {
                    Ingrediente = new IngredienteResponseDto
                    {
                        Id = pi.Ingrediente!.Id,
                        Nome = pi.Ingrediente!.Nome,
                        Raridade = pi.Ingrediente!.Raridade
                    },
                    QuantidadeNecessaria = pi.QuantidadeNecessaria
                }).ToList()
        };
    }
}
