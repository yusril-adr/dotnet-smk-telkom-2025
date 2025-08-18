using System.Net;

namespace dotnet_smk_telkom_2025.Infrastructure.Exceptions;

public class UnauthorizedException : AppException
{
  public UnauthorizedException(string message)
    : base(message, HttpStatusCode.Unauthorized)
  {
  }
}