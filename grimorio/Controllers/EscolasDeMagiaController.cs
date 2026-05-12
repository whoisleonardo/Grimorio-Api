using GrimorioDigital.DTOs;
using GrimorioDigital.Services;
using Microsoft.AspNetCore.Mvc;

namespace GrimorioDigital.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EscolasDeMagiaController : ControllerBase
{
    private readonly IEscolaDeMagiaService _service;

    public EscolasDeMagiaController(IEscolaDeMagiaService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<EscolaDeMagiaResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<EscolaDeMagiaResponseDto>>> GetAll()
    {
        var escolas = await _service.GetAllAsync();
        return Ok(escolas);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(EscolaDeMagiaResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EscolaDeMagiaResponseDto>> GetById(int id)
    {
        var escola = await _service.GetByIdAsync(id);
        if (escola == null)
            return NotFound();

        return Ok(escola);
    }

    [HttpPost]
    [ProducesResponseType(typeof(EscolaDeMagiaResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EscolaDeMagiaResponseDto>> Create([FromBody] EscolaDeMagiaCreateDto dto)
    {
        var escolaCriada = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = escolaCriada.Id }, escolaCriada);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] EscolaDeMagiaUpdateDto dto)
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
