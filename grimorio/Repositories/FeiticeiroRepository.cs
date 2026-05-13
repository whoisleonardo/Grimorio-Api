using GrimorioDigital.Data;
using GrimorioDigital.Models;
using Microsoft.EntityFrameworkCore;

namespace GrimorioDigital.Repositories;

public class FeiticeiroRepository : IFeiticeiroRepository
{
    private readonly AppDbContext _context;

    public FeiticeiroRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Feiticeiro>> GetAllAsync()
    {
        return await _context.Feiticeiros
            .Include(f => f.EscolaDeMagia)
            .Include(f => f.Usuario)
            .ToListAsync();
    }

    public async Task<Feiticeiro?> GetByIdAsync(int id)
    {
        return await _context.Feiticeiros
            .Include(f => f.EscolaDeMagia)
            .Include(f => f.Usuario)
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<Feiticeiro> CreateAsync(Feiticeiro feiticeiro)
    {
        _context.Feiticeiros.Add(feiticeiro);
        await _context.SaveChangesAsync();
        return feiticeiro;
    }

    public async Task<Feiticeiro> UpdateAsync(Feiticeiro feiticeiro)
    {
        _context.Feiticeiros.Update(feiticeiro);
        await _context.SaveChangesAsync();
        return feiticeiro;
    }

    public async Task DeleteAsync(int id)
    {
        var feiticeiro = await GetByIdAsync(id);
        if (feiticeiro != null)
        {
            _context.Feiticeiros.Remove(feiticeiro);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> EscolaDeMagiaExistsAsync(int escolaDeMagiaId)
    {
        return await _context.EscolasDeMagia.AnyAsync(e => e.Id == escolaDeMagiaId);
    }

    public async Task<bool> UsuarioExistsAsync(int usuarioId)
    {
        return await _context.Usuarios.AnyAsync(u => u.Id == usuarioId);
    }
}
