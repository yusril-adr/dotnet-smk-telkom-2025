using System.Net;

namespace dotnet_smk_telkom_2025.Infrastructure.Exceptions;

public class UnAuthenticatedException : AppException
{
  public UnAuthenticatedException(string message)
    : base(message, HttpStatusCode.Unauthorized)
  {
  }
}