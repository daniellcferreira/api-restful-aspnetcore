using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GerenciardorUsuario.Api.Controllers
{
  [Route("/api/login")]
  [ApiController]
  public class LoginController : ControllerBase
  {
    [HttpPost]
    public IActionResult GerarToken()
    {
      // Definição da chave de criptografia
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("bXlT4X2lallYaJldEtleUFsYW5nMjAyNCMh"));
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      // Definição das claims
      var claims = new[]
      {
        new Claim(ClaimTypes.Email, "usuario@email.com"),
        new Claim(ClaimTypes.Role, "Admin"),
        new Claim("ler-dados-por-id", "true")
      };

      // Definição do token
      var token = new JwtSecurityToken(
        issuer: "usuarios-api",
        audience: "usuarios-api",
        claims: claims,
        expires: DateTime.Now.AddMinutes(30),
        signingCredentials: creds
      );

      var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
      return Ok(tokenString);
    }
  }
}