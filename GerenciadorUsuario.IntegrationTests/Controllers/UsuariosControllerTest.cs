using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using System.Net;
using GerenciadorUsuario.IntegrationTests.Factories;
using System.Net.Http.Json;
using GerenciadorUsuario.Api.Models;

namespace GerenciadorUsuario.IntegrationTests.Controllers
{
  public class UsuarioControllerTest : IClassFixture<TestWebApplicationFactory>
  {
    private readonly TestWebApplicationFactory _webApplicationFactory;

    public UsuarioControllerTest(TestWebApplicationFactory webApplicationFactory)
    {
      _webApplicationFactory = webApplicationFactory;
    }

    [Fact]
    public async Task DadoBuscarUsuarios_QuandoRequisitadoAPesquisa_EntaoDevolverListaUsuarios()
    {
      // Arrange
      HttpClient client = _webApplicationFactory.CreateClient();

      // Act
      var response = await client.GetAsync("/api/v1/usuarios");

      // Assert
      response.StatusCode.Should().Be(HttpStatusCode.OK);
      var usuarios = await response.Content.ReadFromJsonAsync<IEnumerable<Usuario>>();
      usuarios.Should().NotBeEmpty();
    }
  }
}
