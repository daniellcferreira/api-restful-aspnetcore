using GerenciadorUsuario.Api.Models;

namespace GerenciadorUsuario.Api.Repository
{
  public interface IUsuarioRepository
  {
    List<Usuario> ObterUsuarios();
  }
}