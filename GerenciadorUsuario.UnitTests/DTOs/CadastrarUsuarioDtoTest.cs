using GerenciadorUsuario.Api.DTOs;
using GerenciadorUsuario.Api.Models;
using FluentAssertions;

namespace GerenciadorUsuario.UnitTests.DTOs
{
  public class CadastrarUsuarioDtoTest
  {
    [Fact(DisplayName = "Testa a conversão do modelo dto para o usuário")]
    public void DadoConverterParaModelo_QuandoSolicitadaUmaConversao_EntaoDeveRetornarUmModeloDeUsuario()
    {
      // Arrange
      CadastrarUsuarioDto dto = new()
      {
        Nome = "Usuario01",
        Email = "usuario01@email.com"
      };

      // Act
      Usuario usuario = dto.ConverterParaModelo();

      // Assert
      usuario.Nome.Should().Be(dto.Nome);
      usuario.Email.Should().Be(dto.Email);
      usuario.Id.Should().NotBeEmpty();
    }
  }
}