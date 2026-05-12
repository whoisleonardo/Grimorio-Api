using GrimorioDigital.DTOs;
using GrimorioDigital.Services;
using Microsoft.AspNetCore.Mvc;

namespace GrimorioDigital.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientesController : ControllerBase
{
    private readonly IIngredienteService _service;

    public IngredientesController(IIngredienteService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<IngredienteResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<IngredienteResponseDto>>> GetAll()
    {
        var ingredientes = await _service.GetAllAsync();
        return Ok(ingredientes);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(IngredienteResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IngredienteResponseDto>> GetById(int id)
    {
        var ingrediente = await _service.GetByIdAsync(id);
        if (ingrediente == null)
            return NotFound();

        return Ok(ingrediente);
    }

    [HttpPost]
    [ProducesResponseType(typeof(IngredienteResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IngredienteResponseDto>> Create([FromBody] IngredienteCreateDto dto)
    {
        var ingredienteCriado = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = ingredienteCriado.Id }, ingredienteCriado);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] IngredienteUpdateDto dto)
    {
        var resultado = await _service.UpdateAsync(id, dto);
        if (resultado == null)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var deletado = await _service.DeleteAsync(id);
        if (!deletado)
            return NotFound();

        return NoContent();
    }
}
