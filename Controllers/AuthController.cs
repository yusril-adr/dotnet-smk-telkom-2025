using dotnet_smk_telkom_2025.Dtos.Parameters;
using dotnet_smk_telkom_2025.Dtos.Results;
using dotnet_smk_telkom_2025.Infrastructure.Dtos;
using dotnet_smk_telkom_2025.Infrastructure.Shared;
using dotnet_smk_telkom_2025.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_smk_telkom_2025.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
  private readonly ILogger<AuthController> _logger;
  private readonly AuthService _authService;
  private readonly AuthUtil _authUtil;
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly IConfiguration _config;

  public AuthController(
    ILogger<AuthController> logger,
    AuthService authService,
    AuthUtil authUtil,
    IHttpContextAccessor httpContextAccessor,
    IConfiguration config
  )
  {
    _logger = logger;
    _authService = authService;
    _authUtil = authUtil;
    _httpContextAccessor = httpContextAccessor;
    _config = config;
  }

  [HttpPost("login/password")]
  public async Task<ApiResponse> Login(
    [FromBody] AuthLoginParameter parameter
  )
  {
    var result = await _authService.Login(parameter);
    return new ApiResponseData<AuthLoginResult>(result);
  }

  [HttpPost("login/token")]
  public async Task<ApiResponse> LoginByToken()
  {
    var token = _authUtil.GetToken(_httpContextAccessor.HttpContext);
    _authUtil.ValidateJwtToken(token, _config["JWTSetting:Secret"]);

    var result = await _authService.LoginByToken(token);
    return new ApiResponseData<UserResult>(result);
  }
}