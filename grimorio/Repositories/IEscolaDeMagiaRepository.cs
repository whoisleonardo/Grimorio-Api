using GrimorioDigital.Models;

namespace GrimorioDigital.Repositories;

public interface IEscolaDeMagiaRepository
{
    Task<List<EscolaDeMagia>> GetAllAsync();
    Task<EscolaDeMagia?> GetByIdAsync(int id);
    Task<EscolaDeMagia> CreateAsync(EscolaDeMagia escolaDeMagia);
    Task<EscolaDeMagia> UpdateAsync(EscolaDeMagia escolaDeMagia);
    Task DeleteAsync(int id);
}
