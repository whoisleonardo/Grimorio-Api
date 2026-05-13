using GrimorioDigital.Models;

namespace GrimorioDigital.Repositories;

public interface IPocaoRepository
{
    Task<List<Pocao>> GetAllAsync();
    Task<Pocao?> GetByIdAsync(int id);
    Task<Pocao> CreateAsync(Pocao pocao);
    Task<Pocao> UpdateAsync(Pocao pocao);
    Task DeleteAsync(int id);
    Task<List<int>> GetExistingIngredientIdsAsync(List<int> ingredientIds);
}
