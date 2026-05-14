using GrimorioDigital.Data;
using GrimorioDigital.DTOs;
using GrimorioDigital.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GrimorioDigital.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MagiasController : ControllerBase
{
    private readonly AppDbContext _context;

    public MagiasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MagiaResponseDto>), 200)]
    public async Task<IActionResult> GetAll()
    {
        var magias = await _context.Magias
            .Include(m => m.EscolaDeMagia)
            .Select(m => new MagiaResponseDto
            {
                Id = m.Id,
                Nome = m.Nome,
                Descricao = m.Descricao,
                Nivel = m.Nivel,
                TipoAlvo = m.TipoAlvo,
                EscolaDeMagiaId = m.EscolaDeMagiaId,
                EscolaDeMagia = new EscolaDeMagiaResponseDto
                {
                    Id = m.EscolaDeMagia.Id,
                    Nome = m.EscolaDeMagia.Nome,
                    Descricao = m.EscolaDeMagia.Descricao,
                    Elemento = m.EscolaDeMagia.Elemento
                }
            })
            .ToListAsync();

        return Ok(magias);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(MagiaResponseDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(int id)
    {
        var magia = await _context.Magias
            .Include(m => m.EscolaDeMagia)
            .Where(m => m.Id == id)
            .Select(m => new MagiaResponseDto
            {
                Id = m.Id,
                Nome = m.Nome,
                Descricao = m.Descricao,
                Nivel = m.Nivel,
                TipoAlvo = m.TipoAlvo,
                EscolaDeMagiaId = m.EscolaDeMagiaId,
                EscolaDeMagia = new EscolaDeMagiaResponseDto
                {
                    Id = m.EscolaDeMagia.Id,
                    Nome = m.EscolaDeMagia.Nome,
                    Descricao = m.EscolaDeMagia.Descricao,
                    Elemento = m.EscolaDeMagia.Elemento
                }
            })
            .FirstOrDefaultAsync();

        if (magia == null)
            return NotFound("Magia não encontrada.");

        return Ok(magia);
    }

    [HttpPost]
    [ProducesResponseType(typeof(MagiaResponseDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Create([FromBody] MagiaCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var escolaExiste = await _context.EscolasDeMagia
            .AnyAsync(e => e.Id == dto.EscolaDeMagiaId);

        if (!escolaExiste)
            return NotFound("EscolaDeMagia não encontrada.");

        var magia = new Magia
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao,
            Nivel = dto.Nivel,
            TipoAlvo = dto.TipoAlvo,
            EscolaDeMagiaId = dto.EscolaDeMagiaId
        };

        _context.Magias.Add(magia);
        await _context.SaveChangesAsync();

        var magiaResponse = await _context.Magias
            .Include(m => m.EscolaDeMagia)
            .Where(m => m.Id == magia.Id)
            .Select(m => new MagiaResponseDto
            {
                Id = m.Id,
                Nome = m.Nome,
                Descricao = m.Descricao,
                Nivel = m.Nivel,
                TipoAlvo = m.TipoAlvo,
                EscolaDeMagiaId = m.EscolaDeMagiaId,
                EscolaDeMagia = new EscolaDeMagiaResponseDto
                {
                    Id = m.EscolaDeMagia.Id,
                    Nome = m.EscolaDeMagia.Nome,
                    Descricao = m.EscolaDeMagia.Descricao,
                    Elemento = m.EscolaDeMagia.Elemento
                }
            })
            .FirstOrDefaultAsync();

        return CreatedAtAction(nameof(GetById), new { id = magia.Id }, magiaResponse);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(int id, [FromBody] MagiaUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var magia = await _context.Magias.FindAsync(id);

        if (magia == null)
            return NotFound("Magia não encontrada.");

        var escolaExiste = await _context.EscolasDeMagia
            .AnyAsync(e => e.Id == dto.EscolaDeMagiaId);

        if (!escolaExiste)
            return NotFound("EscolaDeMagia não encontrada.");

        magia.Nome = dto.Nome;
        magia.Descricao = dto.Descricao;
        magia.Nivel = dto.Nivel;
        magia.TipoAlvo = dto.TipoAlvo;
        magia.EscolaDeMagiaId = dto.EscolaDeMagiaId;

        _context.Magias.Update(magia);
        await _context.SaveChangesAsync();

        var magiaResponse = await _context.Magias
            .Include(m => m.EscolaDeMagia)
            .Where(m => m.Id == id)
            .Select(m => new MagiaResponseDto
            {
                Id = m.Id,
                Nome = m.Nome,
                Descricao = m.Descricao,
                Nivel = m.Nivel,
                TipoAlvo = m.TipoAlvo,
                EscolaDeMagiaId = m.EscolaDeMagiaId,
                EscolaDeMagia = new EscolaDeMagiaResponseDto
                {
                    Id = m.EscolaDeMagia.Id,
                    Nome = m.EscolaDeMagia.Nome,
                    Descricao = m.EscolaDeMagia.Descricao,
                    Elemento = m.EscolaDeMagia.Elemento
                }
            })
            .FirstOrDefaultAsync();

        return Ok(magiaResponse);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(int id)
    {
        var magia = await _context.Magias.FindAsync(id);

        if (magia == null)
            return NotFound("Magia não encontrada.");

        _context.Magias.Remove(magia);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
