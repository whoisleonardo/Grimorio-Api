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
public class PocoesController : ControllerBase
{
    // Injeção de dependência via construtor — mesmo padrão de AuthController e UsuariosController
    private readonly AppDbContext _context;

    public PocoesController(AppDbContext context)
    {
        _context = context;
    }

    // =========================================================
    // GET /api/pocoes
    // Retorna todas as poções com seus ingredientes expandidos
    // =========================================================
    /// <summary>
    /// Lista todas as poções com seus ingredientes
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PocaoResponseDto>), 200)]
    public async Task<IActionResult> GetAll()
    {
        // Include(p => p.PocaoIngredientes) carrega a tabela pivot
        // ThenInclude(pi => pi.Ingrediente) carrega o Ingrediente referenciado por cada linha da pivot
        var pocoes = await _context.Pocoes
            .Include(p => p.PocaoIngredientes)
                .ThenInclude(pi => pi.Ingrediente)
            .ToListAsync();

        var resultado = pocoes.Select(p => new PocaoResponseDto
        {
            Id = p.Id,
            Nome = p.Nome,
            Efeito = p.Efeito,
            DuracaoMinutos = p.DuracaoMinutos,
            Ingredientes = p.PocaoIngredientes
                .Where(pi => pi.Ingrediente != null)
                .Select(pi => new PocaoIngredienteResponseDto
                {
                    Ingrediente = new IngredienteResponseDto
                    {
                        Id = pi.Ingrediente!.Id,
                        Nome = pi.Ingrediente!.Nome,
                        Raridade = pi.Ingrediente!.Raridade
                    },
                    QuantidadeNecessaria = pi.QuantidadeNecessaria
                }).ToList()
        });

        return Ok(resultado);
    }

    // =========================================================
    // GET /api/pocoes/{id}
    // Retorna uma poção específica ou 404
    // =========================================================
    /// <summary>
    /// Busca uma poção por ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PocaoResponseDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(int id)
    {
        var pocao = await _context.Pocoes
            .Include(p => p.PocaoIngredientes)
                .ThenInclude(pi => pi.Ingrediente)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pocao == null)
            return NotFound(new { mensagem = "Poção não encontrada" });

        return Ok(new PocaoResponseDto
        {
            Id = pocao.Id,
            Nome = pocao.Nome,
            Efeito = pocao.Efeito,
            DuracaoMinutos = pocao.DuracaoMinutos,
            Ingredientes = pocao.PocaoIngredientes
                .Where(pi => pi.Ingrediente != null)
                .Select(pi => new PocaoIngredienteResponseDto
                {
                    Ingrediente = new IngredienteResponseDto
                    {
                        Id = pi.Ingrediente!.Id,
                        Nome = pi.Ingrediente!.Nome,
                        Raridade = pi.Ingrediente!.Raridade
                    },
                    QuantidadeNecessaria = pi.QuantidadeNecessaria
                }).ToList()
        });
    }

    // =========================================================
    // POST /api/pocoes
    // Cria uma nova poção e insere na tabela pivot
    // =========================================================
    /// <summary>
    /// Cria uma nova poção com ingredientes
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(PocaoResponseDto), 201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] PocaoCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Valida que ao menos 1 ingrediente foi enviado
        if (dto.Ingredientes == null || dto.Ingredientes.Count == 0)
            return BadRequest(new { mensagem = "A poção deve ter pelo menos 1 ingrediente" });

        // Verifica se há ingredientes duplicados no próprio payload
        var idsNoPayload = dto.Ingredientes.Where(i => i.IngredienteId > 0).Select(i => i.IngredienteId).ToList();
        if (idsNoPayload.Count != idsNoPayload.Distinct().Count())
            return BadRequest(new { mensagem = "Há ingredientes duplicados na lista" });

        // Verifica se todos os IDs de ingredientes existem no banco
        var idsExistentes = await _context.Ingredientes
            .Where(i => idsNoPayload.Contains(i.Id))
            .Select(i => i.Id)
            .ToListAsync();

        var idsInvalidos = idsNoPayload.Except(idsExistentes).ToList();
        if (idsInvalidos.Any())
            return BadRequest(new
            {
                mensagem = "Ingredientes não encontrados",
                idsInvalidos
            });

        var pocao = new Pocao
        {
            Nome = dto.Nome,
            Efeito = dto.Efeito,
            DuracaoMinutos = dto.DuracaoMinutos
        };

        // Adiciona a Pocao ao contexto — o EF vai gerar o Id automaticamente
        _context.Pocoes.Add(pocao);

        // SaveChanges aqui gera o Id da poção, necessário para criar as linhas na pivot
        await _context.SaveChangesAsync();

        // Cria ingredientes novos (IngredienteId == 0)
        foreach (var ingredienteDto in dto.Ingredientes.Where(i => i.IngredienteId == 0))
        {
            _context.Ingredientes.Add(new Ingrediente
            {
                Nome = ingredienteDto.NomeIngrediente,
                Raridade = ingredienteDto.Raridade
            });
        }
        await _context.SaveChangesAsync();

        foreach (var ingredienteDto in dto.Ingredientes)
        {
            int ingredienteId = ingredienteDto.IngredienteId;

            // Se IngredienteId é 0, busca o ingrediente criado pelo nome
            if (ingredienteId == 0)
            {
                var ingrediente = await _context.Ingredientes
                    .FirstOrDefaultAsync(i => i.Nome == ingredienteDto.NomeIngrediente);
                if (ingrediente != null)
                    ingredienteId = ingrediente.Id;
            }

            var pocaoIngrediente = new PocaoIngrediente
            {
                PocaoId = pocao.Id,
                IngredienteId = ingredienteId,
                QuantidadeNecessaria = ingredienteDto.QuantidadeNecessaria
            };
            _context.PocaoIngredientes.Add(pocaoIngrediente);
        }

        await _context.SaveChangesAsync();

        // Recarrega a poção com os ingredientes para montar o response
        var pocaoCriada = await _context.Pocoes
            .Include(p => p.PocaoIngredientes)
                .ThenInclude(pi => pi.Ingrediente)
            .FirstOrDefaultAsync(p => p.Id == pocao.Id);

        if (pocaoCriada == null)
            return BadRequest(new { mensagem = "Erro ao criar a poção" });

        return CreatedAtAction(nameof(GetById), new { id = pocao.Id }, new PocaoResponseDto
        {
            Id = pocaoCriada.Id,
            Nome = pocaoCriada.Nome,
            Efeito = pocaoCriada.Efeito,
            DuracaoMinutos = pocaoCriada.DuracaoMinutos,
            Ingredientes = pocaoCriada.PocaoIngredientes
                .Where(pi => pi.Ingrediente != null)
                .Select(pi => new PocaoIngredienteResponseDto
                {
                    Ingrediente = new IngredienteResponseDto
                    {
                        Id = pi.Ingrediente!.Id,
                        Nome = pi.Ingrediente!.Nome,
                        Raridade = pi.Ingrediente!.Raridade
                    },
                    QuantidadeNecessaria = pi.QuantidadeNecessaria
                }).ToList()
        });

    }

    // =========================================================
    // PUT /api/pocoes/{id}
    // Atualiza dados da poção E substitui TODOS os ingredientes
    // =========================================================
    /// <summary>
    /// Atualiza uma poção existente (substitui todos os ingredientes)
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(PocaoResponseDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(int id, [FromBody] PocaoUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (dto.Ingredientes == null || dto.Ingredientes.Count == 0)
            return BadRequest(new { mensagem = "A poção deve ter pelo menos 1 ingrediente" });

        var idsNoPayload = dto.Ingredientes.Where(i => i.IngredienteId > 0).Select(i => i.IngredienteId).ToList();
        if (idsNoPayload.Count != idsNoPayload.Distinct().Count())
            return BadRequest(new { mensagem = "Há ingredientes duplicados na lista" });

        // Busca a poção com os ingredientes atuais já carregados
        var pocao = await _context.Pocoes
            .Include(p => p.PocaoIngredientes)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pocao == null)
            return NotFound(new { mensagem = "Poção não encontrada" });

        // Valida os ingredientes do novo payload (apenas existentes)
        var idsExistentes = await _context.Ingredientes
            .Where(i => idsNoPayload.Contains(i.Id))
            .Select(i => i.Id)
            .ToListAsync();

        var idsInvalidos = idsNoPayload.Except(idsExistentes).ToList();
        if (idsInvalidos.Any())
            return BadRequest(new { mensagem = "Ingredientes não encontrados", idsInvalidos });

        // Atualiza os campos da poção
        pocao.Nome = dto.Nome;
        pocao.Efeito = dto.Efeito;
        pocao.DuracaoMinutos = dto.DuracaoMinutos;

        // ESTRATÉGIA DO PUT: remover todos os ingredientes antigos e inserir os novos
        _context.PocaoIngredientes.RemoveRange(pocao.PocaoIngredientes);

        // Cria ingredientes novos (IngredienteId == 0)
        foreach (var ingredienteDto in dto.Ingredientes.Where(i => i.IngredienteId == 0))
        {
            _context.Ingredientes.Add(new Ingrediente
            {
                Nome = ingredienteDto.NomeIngrediente,
                Raridade = ingredienteDto.Raridade
            });
        }
        await _context.SaveChangesAsync();

        foreach (var ingredienteDto in dto.Ingredientes)
        {
            int ingredienteId = ingredienteDto.IngredienteId;

            // Se IngredienteId é 0, busca o ingrediente criado pelo nome
            if (ingredienteId == 0)
            {
                var ingrediente = await _context.Ingredientes
                    .FirstOrDefaultAsync(i => i.Nome == ingredienteDto.NomeIngrediente);
                if (ingrediente != null)
                    ingredienteId = ingrediente.Id;
            }

            _context.PocaoIngredientes.Add(new PocaoIngrediente
            {
                PocaoId = pocao.Id,
                IngredienteId = ingredienteId,
                QuantidadeNecessaria = ingredienteDto.QuantidadeNecessaria
            });
        }

        await _context.SaveChangesAsync();

        var pocaoAtualizada = await _context.Pocoes
            .Include(p => p.PocaoIngredientes)
                .ThenInclude(pi => pi.Ingrediente)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pocaoAtualizada == null)
            return BadRequest(new { mensagem = "Erro ao atualizar a poção" });

        return Ok(new PocaoResponseDto
        {
            Id = pocaoAtualizada.Id,
            Nome = pocaoAtualizada.Nome,
            Efeito = pocaoAtualizada.Efeito,
            DuracaoMinutos = pocaoAtualizada.DuracaoMinutos,
            Ingredientes = pocaoAtualizada.PocaoIngredientes
                .Where(pi => pi.Ingrediente != null)
                .Select(pi => new PocaoIngredienteResponseDto
                {
                    Ingrediente = new IngredienteResponseDto
                    {
                        Id = pi.Ingrediente!.Id,
                        Nome = pi.Ingrediente!.Nome,
                        Raridade = pi.Ingrediente!.Raridade
                    },
                    QuantidadeNecessaria = pi.QuantidadeNecessaria
                }).ToList()
        });
    }

    // =========================================================
    // DELETE /api/pocoes/{id}
    // Remove a poção (e em cascata as linhas da pivot)
    // =========================================================
    /// <summary>
    /// Remove uma poção
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(int id)
    {
        // Carregamos com Include para que o EF Core rastreie e delete as linhas da pivot junto
        var pocao = await _context.Pocoes
            .Include(p => p.PocaoIngredientes)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pocao == null)
            return NotFound(new { mensagem = "Poção não encontrada" });

        _context.Pocoes.Remove(pocao);
        await _context.SaveChangesAsync();

        return NoContent(); // 204 — sem body
    }

    // =========================================================
    // Método privado auxiliar: converte Model → DTO de resposta
    // Centraliza o mapeamento para não repetir em cada endpoint
    // =========================================================
    private static PocaoResponseDto MapToResponseDto(Pocao pocao)
    {
        return new PocaoResponseDto
        {
            Id = pocao.Id,
            Nome = pocao.Nome,
            Efeito = pocao.Efeito,
            DuracaoMinutos = pocao.DuracaoMinutos,
            Ingredientes = pocao.PocaoIngredientes
                .Where(pi => pi.Ingrediente != null)
                .Select(pi => new PocaoIngredienteResponseDto
                {
                    Ingrediente = new IngredienteResponseDto
                    {
                        Id = pi.Ingrediente!.Id,
                        Nome = pi.Ingrediente!.Nome,
                        Raridade = pi.Ingrediente!.Raridade
                    },
                    QuantidadeNecessaria = pi.QuantidadeNecessaria
                }).ToList()
        };
    }
}

