using Microsoft.AspNetCore.Authorization;
using dotnet_smk_telkom_2025.Infrastructure.Shared;
using dotnet_smk_telkom_2025.Dtos.Results;

namespace dotnet_smk_telkom_2025.Infrastructure.Middlewares;

public class AuthorizationMiddleware(
  RequestDelegate next,
  IConfiguration config,
  AuthUtil authUtil
)
{
  private readonly RequestDelegate _next = next;
  private readonly IConfiguration _config = config;
  private readonly AuthUtil _authUtil = authUtil;

  public async Task Invoke(HttpContext context)
  {
    var endpoint = context.GetEndpoint();
    if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() is object)
    {
      await _next(context);
      return;
    }

    var token = _authUtil.GetToken(context);
    _authUtil.ValidateJwtToken(token, _config["JWTSetting:Secret"]);

    var user = _authUtil.GetUserLogged<UserResult>(token);
    context.User = _authUtil.ClaimPrincipalWithJson(user);

    await _next(context);
  }
}

