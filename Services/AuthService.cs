using dotnet_smk_telkom_2025.Dtos.Parameters;
using dotnet_smk_telkom_2025.Dtos.Results;
using dotnet_smk_telkom_2025.Infrastructure.Exceptions;
using dotnet_smk_telkom_2025.Infrastructure.Shared;
using dotnet_smk_telkom_2025.Models;
using dotnet_smk_telkom_2025.Repositories;
using BC = BCrypt.Net.BCrypt;

namespace dotnet_smk_telkom_2025.Services;

public class AuthService
{
  private readonly AuthUtil _authUtil;
  private readonly UserQueryRepository _userQueryRepository;
  private readonly IConfiguration _config;

  public AuthService(
    AuthUtil authUtil,
    UserQueryRepository userQueryRepository,
    IConfiguration config
  )
  {
    _authUtil = authUtil;
    _userQueryRepository = userQueryRepository;
    _config = config;
  }

  public async Task<AuthLoginResult> Login(
    AuthLoginParameter parameter
  )
  {
    var user = await _userQueryRepository.FindOneByEmail(parameter.Email);
    if (user == null)
    {
      throw new UnAuthenticatedException("Email or password is invalid");
    }

    var isPasswordValid = BC.Verify(parameter.Password, user.Password);
    if (!isPasswordValid)
    {
      throw new UnAuthenticatedException("Email or password is invalid");
    }

    var token = _authUtil.GenerateJwtToken(
      _config["JWTSetting:Secret"],
      _authUtil.GenerateJWTClaimInfo(user)
    );

    return new AuthLoginResult(token);
  }

  public async Task<UserResult> LoginByToken(
    string token
  )
  {
    var result = _authUtil.GetUserLogged<UserResult>(token);

    // Get Updated User from database
    var updatedUser = await _userQueryRepository.FindOneById(result.Id);
    return new UserResult(updatedUser);
  }
}
