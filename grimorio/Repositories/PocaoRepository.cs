using GrimorioDigital.Data;
using GrimorioDigital.Models;
using Microsoft.EntityFrameworkCore;

namespace GrimorioDigital.Repositories;

public class PocaoRepository : IPocaoRepository
{
    private readonly AppDbContext _context;

    public PocaoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Pocao>> GetAllAsync()
    {
        return await _context.Pocoes
            .Include(p => p.PocaoIngredientes)
                .ThenInclude(pi => pi.Ingrediente)
            .ToListAsync();
    }

    public async Task<Pocao?> GetByIdAsync(int id)
    {
        return await _context.Pocoes
            .Include(p => p.PocaoIngredientes)
                .ThenInclude(pi => pi.Ingrediente)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Pocao> CreateAsync(Pocao pocao)
    {
        _context.Pocoes.Add(pocao);
        await _context.SaveChangesAsync();
        return pocao;
    }

    public async Task<Pocao> UpdateAsync(Pocao pocao)
    {
        _context.Pocoes.Update(pocao);
        await _context.SaveChangesAsync();
        return pocao;
    }

    public async Task DeleteAsync(int id)
    {
        var pocao = await GetByIdAsync(id);
        if (pocao != null)
        {
            _context.Pocoes.Remove(pocao);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<int>> GetExistingIngredientIdsAsync(List<int> ingredientIds)
    {
        return await _context.Ingredientes
            .Where(i => ingredientIds.Contains(i.Id))
            .Select(i => i.Id)
            .ToListAsync();
    }
}
