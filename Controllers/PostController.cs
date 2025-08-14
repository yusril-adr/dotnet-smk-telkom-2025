using System.Net;
using dotnet_smk_telkom_2025.Dtos.Parameters;
using dotnet_smk_telkom_2025.Dtos.Results;
using dotnet_smk_telkom_2025.Infrastructure.Dtos;
using dotnet_smk_telkom_2025.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_smk_telkom_2025.Controllers;

[ApiController]
[Route("posts")]
public class PostController : ControllerBase
{
  private readonly ILogger<PostController> _logger;
  private readonly PostService _postService;

  public PostController(
    ILogger<PostController> logger,
    PostService postService
  )
  {
    _logger = logger;
    _postService = postService;
  }

  [HttpGet]
  public ApiResponse GetAll()
  {
    var results = _postService.GetAll();
    return new ApiResponseList<PostResult>(results);
  }

  [HttpGet("{id}")]
  public ApiResponse FindOneById(Guid id)
  {
    var result = _postService.FindOneById(id);
    return new ApiResponseData<PostResult>(result);
  }

  [HttpPost]
  public ApiResponse Create(
    [FromBody] PostCreateParameter parameter
  )
  {
    var result = _postService.Create(parameter);
    return new ApiResponseData<PostResult>(result, HttpStatusCode.Created);
  }

  [HttpPatch("{id}")]
  public ApiResponse Update(
    Guid id,
    [FromBody] PostUpdateParameter parameter
  )
  {
    var result = _postService.Update(id, parameter);
    return new ApiResponseData<PostResult>(result);
  }

  [HttpDelete("{id}")]
  public ApiResponse Delete(
    Guid id
  )
  {
    _postService.Delete(id);

    return new ApiResponseData<PostResult>(null);
  }
}