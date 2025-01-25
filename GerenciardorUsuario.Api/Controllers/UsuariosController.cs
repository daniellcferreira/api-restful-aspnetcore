using GerenciadorUsuario.Api.DTOs;
using GerenciadorUsuario.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorUsuario.Api.Controllers
{
  [Route("/api/usuarios")]
  [Produces("application/json")]
  [Consumes("application/json")]
  [ApiController]
  public class UsuariosController : ControllerBase
  {
    private readonly static List<Usuario> _usuarios = new List<Usuario>()
    {
      new Usuario()
      {
        Id = Guid.NewGuid(),
        Nome = "Usuario1",
        Email = "usuario01.email.com"
      }
    };

    [HttpGet]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK, Type = typeof(List<Usuario>))]
    public IActionResult BuscarUsuarios([FromQuery] string filtroNome = "")
    {
      IEnumerable<Usuario> usuariosFiltrados = _usuarios.Where(x => x.Nome.StartsWith(filtroNome, StringComparison.OrdinalIgnoreCase));
      return Ok(usuariosFiltrados);
    }

    [HttpGet("{id:guid}", Name = nameof(BuscarPorId))]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK, Type = typeof(Usuario))]
    [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
    public IActionResult BuscarPorId([FromRoute] Guid id)
    {
      Usuario usuario = _usuarios.FirstOrDefault(x => x.Id == id);
      if (usuario is not null)
      {
        return Ok(usuario);
      }

      return NotFound();
    }

    [HttpPost]
    [ProducesResponseType(statusCode: StatusCodes.Status201Created, Type = typeof(Usuario))]
    public IActionResult CriarUsuario([FromBody] CadastrarUsuarioDto dto)
    {
      Usuario usuario = dto.ConverterParaModelo();
      _usuarios.Add(usuario);
      return CreatedAtAction(nameof(BuscarPorId), new { usuario.Id }, usuario);
    }

    [HttpPatch("{id:guid}")]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
    [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
    public IActionResult AtualizarUsuario([FromRoute] Guid id, [FromBody] AtualizarUsuarioDto dto)
    {
      Usuario usuario = _usuarios.FirstOrDefault(x => x.Id == id);
      if (usuario is null)
      {
        return NotFound();
      }

      usuario.Nome = dto.Nome;
      return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
    public IActionResult RemoverUsuario([FromRoute] Guid id)
    {
      Usuario usuario = _usuarios.FirstOrDefault(x => x.Id == id);
      if (usuario is null)
      {
        return NotFound();
      }

      _usuarios.Remove(usuario);
      return NoContent();
    }
  }
}