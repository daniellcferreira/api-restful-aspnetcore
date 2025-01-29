using AutoFixture;
using FluentAssertions;
using GerenciadorUsuario.Api.Controllers;
using GerenciadorUsuario.Api.Models;
using GerenciadorUsuario.Api.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq.AutoMock;

namespace GerenciadorUsuario.UnitTests.Controllers
{
  public class UsuarioControllerTest
  {
    [Fact]
    public void DadoBuscarUsuarios_QuandoSolicitadaABusca_EntaoRetorneListaUsuarios()
    {
      // Arrange
      AutoMocker mocker = new();
      UsuariosController controller = mocker.CreateInstance<UsuariosController>();
      Fixture fixture = new();

      List<Usuario> usuariosFake = fixture.Create<List<Usuario>>();
      mocker
        .GetMock<IUsuarioRepository>()
        .Setup(x => x.ObterUsuarios())
        .Returns(usuariosFake);

      // Act
      var response = controller.BuscarUsuarios() as OkObjectResult;
      IEnumerable<Usuario> usuariosRetornados = response.Value as IEnumerable<Usuario>;

      // Assert
      response.Should().NotBeNull();
      usuariosRetornados.Should().BeEquivalentTo(usuariosFake);
    }
  }
}