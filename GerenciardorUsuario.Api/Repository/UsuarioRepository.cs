using GerenciadorUsuario.Api.Models;

namespace GerenciadorUsuario.Api.Repository
{
  public class UsuarioRepository : IUsuarioRepository
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
    public List<Usuario> ObterUsuarios()
    {
      return _usuarios;
    }
  }
}