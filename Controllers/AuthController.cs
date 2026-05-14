using GrimorioDigital.Data;
using GrimorioDigital.DTOs;
using GrimorioDigital.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace GrimorioDigital.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly TokenService _tokenService;

    public AuthController(AppDbContext context, TokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    /// <summary>
    /// Realiza login e retorna um token JWT
    /// </summary>
    [HttpPost("login")]
    [ProducesResponseType(typeof(TokenResponseDto), 200)]
    [ProducesResponseType(401)]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.SenhaHash))
            return Unauthorized(new { mensagem = "Email ou senha inválidos" });

        var (token, expiracao) = _tokenService.GerarToken(usuario);

        return Ok(new TokenResponseDto
        {
            Token = token,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Role = usuario.Role,
            Expiracao = expiracao
        });
    }
}
