using GrimorioDigital.Models;

namespace GrimorioDigital.Repositories;

public interface IFeiticeiroRepository
{
    Task<List<Feiticeiro>> GetAllAsync();
    Task<Feiticeiro?> GetByIdAsync(int id);
    Task<Feiticeiro> CreateAsync(Feiticeiro feiticeiro);
    Task<Feiticeiro> UpdateAsync(Feiticeiro feiticeiro);
    Task DeleteAsync(int id);
    Task<bool> EscolaDeMagiaExistsAsync(int escolaDeMagiaId);
    Task<bool> UsuarioExistsAsync(int usuarioId);
}
