using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_smk_telkom_2025.Infrastructure.Dtos;

public abstract class ApiResponse : ObjectResult
{
  public string Version { get; set; }

  public ApiResponse(
    object value = null
  ) : base(value)
  {
    Value = value;
  }

  public void MapValue(object data)
  {
    Value = new { StatusCode, data, Version };
  }
}

public class ApiResponseData<T> : ApiResponse
{
  public ApiResponseData(
    T data,
    HttpStatusCode statusCode = HttpStatusCode.OK,
    string version = "1.0.0"
  )
  {
    StatusCode = (int)statusCode;
    Version = version;

    MapValue(data);
  }
}

public class ApiResponseList<T> : ApiResponse
{
  public ApiResponseList(
    List<T> items,
    HttpStatusCode statusCode = HttpStatusCode.OK,
    string version = "1.0.0"
  )
  {
    StatusCode = (int)statusCode;
    Version = version;
    var meta = new { items.Count };
    MapValue(new { items, meta });
  }
}
