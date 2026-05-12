using GrimorioDigital.Data;
using GrimorioDigital.Models;
using Microsoft.EntityFrameworkCore;

namespace GrimorioDigital.Repositories;

public class EscolaDeMagiaRepository : IEscolaDeMagiaRepository
{
    private readonly AppDbContext _context;

    public EscolaDeMagiaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<EscolaDeMagia>> GetAllAsync()
    {
        return await _context.EscolasDeMagia.ToListAsync();
    }

    public async Task<EscolaDeMagia?> GetByIdAsync(int id)
    {
        return await _context.EscolasDeMagia.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<EscolaDeMagia> CreateAsync(EscolaDeMagia escolaDeMagia)
    {
        _context.EscolasDeMagia.Add(escolaDeMagia);
        await _context.SaveChangesAsync();
        return escolaDeMagia;
    }

    public async Task<EscolaDeMagia> UpdateAsync(EscolaDeMagia escolaDeMagia)
    {
        _context.EscolasDeMagia.Update(escolaDeMagia);
        await _context.SaveChangesAsync();
        return escolaDeMagia;
    }

    public async Task DeleteAsync(int id)
    {
        var escolaDeMagia = await GetByIdAsync(id);
        if (escolaDeMagia != null)
        {
            _context.EscolasDeMagia.Remove(escolaDeMagia);
            await _context.SaveChangesAsync();
        }
    }
}
