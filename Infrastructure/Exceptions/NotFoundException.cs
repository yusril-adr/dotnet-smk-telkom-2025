using System.Net;

namespace dotnet_smk_telkom_2025.Infrastructure.Exceptions;

public class NotFoundException : AppException
{
  public NotFoundException(string message)
    : base(message, HttpStatusCode.NotFound)
  {
  }
}