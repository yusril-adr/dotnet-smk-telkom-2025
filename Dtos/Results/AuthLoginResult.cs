namespace dotnet_smk_telkom_2025.Dtos.Results;

public class AuthLoginResult
{
  public string AccessToken { get; set; }

  public AuthLoginResult(
    string accessToken
  )
  {
    AccessToken = accessToken;
  }
}
