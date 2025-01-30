using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;

namespace GerenciadorUsuario.IntegrationTests.Fakes
{
  public class FakePolicyEvaluator : IPolicyEvaluator
  {
    public Task<AuthenticateResult> AuthenticateAsync(AuthorizationPolicy policy, HttpContext context)
    {
      var principal = new ClaimsPrincipal();
      var ticket = new AuthenticationTicket(principal, "FakeScheme");
      var result = AuthenticateResult.Success(ticket);
      return Task.FromResult(result);
    }

    public Task<PolicyAuthorizationResult> AuthorizeAsync(AuthorizationPolicy policy, AuthenticateResult authenticationResult, HttpContext context, object resource)
    {
      return Task.FromResult(PolicyAuthorizationResult.Success());
    }
  }
}