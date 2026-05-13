using GrimorioDigital.Data;
using GrimorioDigital.Models;
using Microsoft.EntityFrameworkCore;

namespace GrimorioDigital.Repositories;

public class IngredienteRepository : IIngredienteRepository
{
    private readonly AppDbContext _context;

    public IngredienteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Ingrediente>> GetAllAsync()
    {
        return await _context.Ingredientes.ToListAsync();
    }

    public async Task<Ingrediente?> GetByIdAsync(int id)
    {
        return await _context.Ingredientes.FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<Ingrediente> CreateAsync(Ingrediente ingrediente)
    {
        _context.Ingredientes.Add(ingrediente);
        await _context.SaveChangesAsync();
        return ingrediente;
    }

    public async Task<Ingrediente> UpdateAsync(Ingrediente ingrediente)
    {
        _context.Ingredientes.Update(ingrediente);
        await _context.SaveChangesAsync();
        return ingrediente;
    }

    public async Task DeleteAsync(int id)
    {
        var ingrediente = await GetByIdAsync(id);
        if (ingrediente != null)
        {
            _context.Ingredientes.Remove(ingrediente);
            await _context.SaveChangesAsync();
        }
    }
}
