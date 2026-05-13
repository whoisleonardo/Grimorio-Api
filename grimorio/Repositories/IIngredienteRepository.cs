using GrimorioDigital.Models;

namespace GrimorioDigital.Repositories;

public interface IIngredienteRepository
{
    Task<List<Ingrediente>> GetAllAsync();
    Task<Ingrediente?> GetByIdAsync(int id);
    Task<Ingrediente> CreateAsync(Ingrediente ingrediente);
    Task<Ingrediente> UpdateAsync(Ingrediente ingrediente);
    Task DeleteAsync(int id);
}
