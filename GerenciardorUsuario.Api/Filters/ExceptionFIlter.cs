using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GerenciadorUsuario.Api.Filters
{
  public class ExceptionFilter : IExceptionFilter
  {
    private const string MensagemDeErro = "Ocorreu um erro, por favor tente novamente!";

    public void OnException(ExceptionContext context)
    {
      var erro = new
      {
        Mensagem = MensagemDeErro
      };

      context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
      context.Result = new JsonResult(erro);
    }
  }
}