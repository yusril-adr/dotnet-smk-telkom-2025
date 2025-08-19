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

public class ResponsePagination<T>
{
  public List<T> Items { get; set; }

  public PaginationMeta Meta { get; set; }

  public ResponsePagination(PaginationResult<T> paginationModel)
  {
    Items = paginationModel.Items;
    Meta = paginationModel.Meta;
  }
}

public class ApiResponsePagination<T> : ApiResponse
{
  public ResponsePagination<T> Data { get; set; }

  public ApiResponsePagination(
    PaginationResult<T> data,
    HttpStatusCode statusCode = HttpStatusCode.OK,
    string version = "1.0.0"
  )
  {
    Data = new ResponsePagination<T>(data);
    StatusCode = (int)statusCode;
    Version = version;

    MapValue(Data);
  }
}
