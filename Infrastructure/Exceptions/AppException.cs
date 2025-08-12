using System.Net;

namespace dotnet_smk_telkom_2025.Infrastructure.Exceptions;

public class AppException : Exception
{
  public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;
  public int StatusCodeInInt { get => (int)StatusCode; }

  public AppException(string message)
    : base(message)
  {
  }

  public AppException(string message, HttpStatusCode httpStatusCode)
    : base(message)
  {
    StatusCode = httpStatusCode;
  }
}