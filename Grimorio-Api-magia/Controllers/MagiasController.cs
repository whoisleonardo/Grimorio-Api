using GrimorioDigital.DTOs;
using GrimorioDigital.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GrimorioDigital.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MagiasController : ControllerBase
{
    private readonly IMagiaService _magiaService;

    public MagiasController(IMagiaService magiaService)
    {
        _magiaService = magiaService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MagiaResponseDto>), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IEnumerable<MagiaResponseDto>>> GetAll()
    {
        try
        {
            var magias = await _magiaService.GetAllMagias();
            return Ok(magias);
        }
        catch (Exception exception)
        {
            return StatusCode(500, "Um erro ocorreu ao recuperar as magias. Por favor, tente novamente mais tarde.");
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(MagiaResponseDto), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<MagiaResponseDto>> GetById(int id)
    {
        try
        {
            var magia = await _magiaService.GetMagiaById(id);
            return Ok(magia);
        }
        catch (KeyNotFoundException exception)
        {
            return NotFound("Magia não encontrada.");
        }
        catch (Exception exception)
        {
            return StatusCode(500, "Um erro ocorreu ao recuperar a magia. Por favor, tente novamente mais tarde.");
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(MagiaResponseDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<MagiaResponseDto>> Create([FromBody] MagiaCreateDto createMagiaDto)
    {
        try
        {
            var magiaResponse = await _magiaService.CreateMagia(createMagiaDto);
            return CreatedAtAction(nameof(GetById), new { id = magiaResponse.Id }, magiaResponse);
        }
        catch (ArgumentException exception)
        {
            return NotFound("EscolaDeMagia não encontrada.");
        }
        catch (Exception exception)
        {
            return StatusCode(500, "Um erro ocorreu ao criar a magia. Por favor, tente novamente mais tarde.");
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(MagiaResponseDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<MagiaResponseDto>> Update(int id, [FromBody] MagiaUpdateDto updateMagiaDto)
    {
        try
        {
            // TODO: [Arquitetura] Camada de serviço acessa DbContext diretamente - implementar padrão Repository
            // TODO: [Arquitetura] Considerar implementar FluentValidation para regras de negócio complexas
            var magiaResponse = await _magiaService.UpdateMagia(id, updateMagiaDto);
            return Ok(magiaResponse);
        }
        catch (KeyNotFoundException exception)
        {
            return NotFound("Magia não encontrada.");
        }
        catch (ArgumentException exception)
        {
            return NotFound("EscolaDeMagia não encontrada.");
        }
        catch (Exception exception)
        {
            return StatusCode(500, "Um erro ocorreu ao atualizar a magia. Por favor, tente novamente mais tarde.");
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
            await _magiaService.DeleteMagia(id);
            return NoContent();
        }
        catch (KeyNotFoundException exception)
        {
            return NotFound("Magia não encontrada.");
        }
        catch (Exception exception)
        {
            return StatusCode(500, "Um erro ocorreu ao deletar a magia. Por favor, tente novamente mais tarde.");
        }
    }
}
