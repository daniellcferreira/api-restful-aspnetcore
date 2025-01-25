using System.ComponentModel.DataAnnotations;

namespace GerenciadorUsuario.Api.DTOs
{
  public record AtualizarUsuarioDto
  {
    [Required(ErrorMessage = "O nome é obrigatório")]
    [MinLength(5, ErrorMessage = "O nome deve ter no mínimo 5 caracteres")]
    public string Nome { get; init; }
  }
}