using System.ComponentModel.DataAnnotations;
using GerenciadorUsuario.Api.Models;

namespace GerenciadorUsuario.Api.DTOs
{
  public record CadastrarUsuarioDto
  {
    [Required]
    [MinLength(5)]
    public string Nome { get; init; }

    [Required]
    [EmailAddress]
    public string Email { get; init; }

    public Usuario ConverterParaModelo()
    {
      return new Usuario()
      {
        Id = Guid.NewGuid(),
        Nome = Nome,
        Email = Email
      };
    }
  }
}