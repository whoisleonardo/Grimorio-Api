using GrimorioDigital.DTOs;
using GrimorioDigital.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GrimorioDigital.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PocoesController : ControllerBase
{
    private readonly IPocaoService _pocaoService;

    public PocoesController(IPocaoService pocaoService)
    {
        _pocaoService = pocaoService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PocaoResponseDto>), 200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var pocoes = await _pocaoService.GetAllAsync();
            return Ok(pocoes);
        }
        catch (Exception)
        {
            return StatusCode(500, "Um erro ocorreu ao recuperar as poções. Por favor, tente novamente mais tarde.");
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PocaoResponseDto), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var pocao = await _pocaoService.GetByIdAsync(id);
            return Ok(pocao);
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Poção não encontrada.");
        }
        catch (Exception)
        {
            return StatusCode(500, "Um erro ocorreu ao recuperar a poção. Por favor, tente novamente mais tarde.");
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(PocaoResponseDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Create([FromBody] PocaoCreateDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pocaoCriada = await _pocaoService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = pocaoCriada.Id }, pocaoCriada);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(new { mensagem = exception.Message });
        }
        catch (KeyNotFoundException exception)
        {
            return BadRequest(new { mensagem = exception.Message });
        }
        catch (InvalidOperationException exception)
        {
            return BadRequest(new { mensagem = exception.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, "Um erro ocorreu ao criar a poção. Por favor, tente novamente mais tarde.");
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(PocaoResponseDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Update(int id, [FromBody] PocaoUpdateDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pocaoAtualizada = await _pocaoService.UpdateAsync(id, dto);
            return Ok(pocaoAtualizada);
        }
        catch (KeyNotFoundException exception)
        {
            return NotFound(new { mensagem = exception.Message });
        }
        catch (ArgumentException exception)
        {
            return BadRequest(new { mensagem = exception.Message });
        }
        catch (InvalidOperationException exception)
        {
            return BadRequest(new { mensagem = exception.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, "Um erro ocorreu ao atualizar a poção. Por favor, tente novamente mais tarde.");
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _pocaoService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException exception)
        {
            return NotFound(new { mensagem = exception.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, "Um erro ocorreu ao deletar a poção. Por favor, tente novamente mais tarde.");
        }
    }
}

