using System.Net;

namespace dotnet_smk_telkom_2025.Infrastructure.Exceptions;

public class BadParameterException : AppException
{
  public BadParameterException(string message)
    : base(message, HttpStatusCode.BadRequest)
  {
  }
}