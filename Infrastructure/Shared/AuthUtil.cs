using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;
using dotnet_smk_telkom_2025.Models;
using dotnet_smk_telkom_2025.Infrastructure.Exceptions;
using dotnet_smk_telkom_2025.Dtos.Results;

namespace dotnet_smk_telkom_2025.Infrastructure.Shared;

public class AuthUtil(
    ILogger<AuthUtil> logger,
    IConfiguration config
)
{
  private readonly ILogger<AuthUtil> _logger = logger;
  private readonly IConfiguration _config = config;

  public string GetToken(HttpContext context)
  {
    var token = string.Empty;
    var headers = context.Request.Headers;
    if (headers.ContainsKey("Authorization") && headers.Authorization.ToString().StartsWith("Bearer "))
    {
      token = headers.Authorization.ToString().Replace("Bearer ", string.Empty);
    }

    return token;
  }

  public string GenerateJwtToken(string secretKey, string userJson, DateTime? expires = null)
  {
    if (expires == null)
    {
      var tokenLifetimeInMinutes = int.Parse(_config["JWTSetting:LifetimeInMinutes"] ?? "60");
      var expiredAt = DateTime.Now.AddMinutes(tokenLifetimeInMinutes);
      expires = expiredAt;
    }

    var tokenHandler = new JwtSecurityTokenHandler();
    var key = GenerateSymetricKey(secretKey);
    var tokenDescriptor = new SecurityTokenDescriptor()
    {
      Subject = new ClaimsIdentity([
        new Claim("user", userJson.ToString())
      ]),
      Expires = expires,
      SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
    };
    var token = tokenHandler.CreateToken(tokenDescriptor);

    return tokenHandler.WriteToken(token);
  }

  public string GenerateJWTClaimInfo(User user)
  {
    return JsonSerializer.Serialize(new UserResult(user));
  }

  public ClaimsPrincipal ClaimPrincipalWithJson(dynamic user)
  {
    var userObject = (Dictionary<string, object>)JsonSerializer.Deserialize<Dictionary<string, object>>(
      JsonSerializer.Serialize(user)
    );

    var claims = userObject
      .Where(kvp => kvp.Key != null && kvp.Value != null)
      .Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()))
      .ToList();

    var identity = new ClaimsIdentity(claims, "User");

    return new ClaimsPrincipal(identity);
  }

  public UserResult GenerateUserAuthInfo(User user)
  {
    return new UserResult(user);
  }

  public SymmetricSecurityKey GenerateSymetricKey(string key)
  {
    var keyBytes = Encoding.UTF8.GetBytes(key);
    if (keyBytes.Length < 32)
    {
      Array.Resize(ref keyBytes, 32);
    }
    return new SymmetricSecurityKey(keyBytes);
  }

  public T GetUserLogged<T>(string token)
  {
    try
    {

      var tokenHandler = new JwtSecurityTokenHandler();
      var tokenDecoded = tokenHandler.ReadToken(token) as JwtSecurityToken;

      var userClaim = tokenDecoded.Claims.First(claim => claim.Type == "user");
      var userValue = userClaim.Value;

      var userObject = JsonSerializer.Deserialize<T>(userValue);

      return userObject;
    }
    catch (Exception err)
    {
      _logger.LogError("Error getting user from token: {err}", err);
      throw new UnAuthenticatedException("Token is invalid or expired");
    }
  }

  public Guid GetUserLoggedId(HttpContext context)
  {
    var token = GetToken(context);
    var user = GetUserLogged<UserResult>(token);
    return user.Id;
  }

  public JwtSecurityToken ValidateJwtToken(string tokenString, string secret)
  {
    try
    {
      var securityKey = GenerateSymetricKey(secret);
      var handler = new JwtSecurityTokenHandler();
      var validation = new TokenValidationParameters()
      {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        LifetimeValidator = CustomLifetimeValidator,
        RequireExpirationTime = true,
        IssuerSigningKey = securityKey,
        ValidateIssuerSigningKey = true,
      };
      var principal = handler.ValidateToken(tokenString, validation, out SecurityToken token);

      return (JwtSecurityToken)token;
    }
    catch (Exception err)
    {
      _logger.LogError("Error validating token: {err}", err);
      throw new UnAuthenticatedException("Token is invalid or expired");
    }
  }

  private static bool CustomLifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken tokenToValidate, TokenValidationParameters @param)
  {
    if (expires != null)
    {
      return expires > DateTime.UtcNow;
    }
    return false;
  }
}