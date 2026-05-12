using GrimorioDigital.DTOs;
using GrimorioDigital.Models;
using GrimorioDigital.Repositories;

namespace GrimorioDigital.Services;

public class FeiticeiroService : IFeiticeiroService
{
    private readonly IFeiticeiroRepository _repository;

    public FeiticeiroService(IFeiticeiroRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<FeiticeiroResponseDto>> GetAllAsync()
    {
        var feiticeiros = await _repository.GetAllAsync();
        return feiticeiros.Select(MapToResponse).ToList();
    }

    public async Task<FeiticeiroResponseDto?> GetByIdAsync(int id)
    {
        var feiticeiro = await _repository.GetByIdAsync(id);
        return feiticeiro == null ? null : MapToResponse(feiticeiro);
    }

    public async Task<FeiticeiroResponseDto> CreateAsync(FeiticeiroCreateDto dto)
    {
        var escolaDeMagiaExists = await _repository.EscolaDeMagiaExistsAsync(dto.EscolaDeMagiaId);
        if (!escolaDeMagiaExists)
            throw new InvalidOperationException($"Escola de Magia com ID {dto.EscolaDeMagiaId} não existe");

        var usuarioExists = await _repository.UsuarioExistsAsync(dto.UsuarioId);
        if (!usuarioExists)
            throw new InvalidOperationException($"Usuário com ID {dto.UsuarioId} não existe");

        var feiticeiro = new Feiticeiro
        {
            Nome = dto.Nome,
            NivelMagico = dto.NivelMagico,
            Especialidade = dto.Especialidade,
            EscolaDeMagiaId = dto.EscolaDeMagiaId,
            UsuarioId = dto.UsuarioId
        };

        var createdFeiticeiro = await _repository.CreateAsync(feiticeiro);
        return MapToResponse(createdFeiticeiro);
    }

    public async Task<FeiticeiroResponseDto?> UpdateAsync(int id, FeiticeiroUpdateDto dto)
    {
        var feiticeiro = await _repository.GetByIdAsync(id);
        if (feiticeiro == null)
            return null;

        var escolaDeMagiaExists = await _repository.EscolaDeMagiaExistsAsync(dto.EscolaDeMagiaId);
        if (!escolaDeMagiaExists)
            throw new InvalidOperationException($"Escola de Magia com ID {dto.EscolaDeMagiaId} não existe");

        var usuarioExists = await _repository.UsuarioExistsAsync(dto.UsuarioId);
        if (!usuarioExists)
            throw new InvalidOperationException($"Usuário com ID {dto.UsuarioId} não existe");

        feiticeiro.Nome = dto.Nome;
        feiticeiro.NivelMagico = dto.NivelMagico;
        feiticeiro.Especialidade = dto.Especialidade;
        feiticeiro.EscolaDeMagiaId = dto.EscolaDeMagiaId;
        feiticeiro.UsuarioId = dto.UsuarioId;

        var updatedFeiticeiro = await _repository.UpdateAsync(feiticeiro);
        return MapToResponse(updatedFeiticeiro);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var feiticeiro = await _repository.GetByIdAsync(id);
        if (feiticeiro == null)
            return false;

        await _repository.DeleteAsync(id);
        return true;
    }

    private static FeiticeiroResponseDto MapToResponse(Feiticeiro feiticeiro)
    {
        return new FeiticeiroResponseDto
        {
            Id = feiticeiro.Id,
            Nome = feiticeiro.Nome,
            NivelMagico = feiticeiro.NivelMagico,
            Especialidade = feiticeiro.Especialidade,
            EscolaDeMagiaId = feiticeiro.EscolaDeMagiaId,
            UsuarioId = feiticeiro.UsuarioId,
            EscolaDeMagia = feiticeiro.EscolaDeMagia == null ? null : new EscolaDeMagiaResponseDto
            {
                Id = feiticeiro.EscolaDeMagia.Id,
                Nome = feiticeiro.EscolaDeMagia.Nome,
                Descricao = feiticeiro.EscolaDeMagia.Descricao,
                Elemento = feiticeiro.EscolaDeMagia.Elemento
            },
            Usuario = feiticeiro.Usuario == null ? null : new UsuarioResponseDto
            {
                Id = feiticeiro.Usuario.Id,
                Nome = feiticeiro.Usuario.Nome,
                Email = feiticeiro.Usuario.Email,
                Role = feiticeiro.Usuario.Role
            }
        };
    }
}
