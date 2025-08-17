using System.Net;
using System.Threading.Tasks;
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
  public async Task<ApiResponse> GetAll()
  {
    var results = await _postService.GetAll();
    return new ApiResponseList<PostResult>(results);
  }

  [HttpGet("{id}")]
  public async Task<ApiResponse> FindOneById(Guid id)
  {
    var result = await _postService.FindOneById(id);
    return new ApiResponseData<PostResult>(result);
  }

  [HttpPost]
  public async Task<ApiResponse> Create(
    [FromBody] PostCreateParameter parameter
  )
  {
    var result = await _postService.Create(parameter);
    return new ApiResponseData<PostResult>(result, HttpStatusCode.Created);
  }

  [HttpPatch("{id}")]
  public async Task<ApiResponse> Update(
    Guid id,
    [FromBody] PostUpdateParameter parameter
  )
  {
    var result = await _postService.Update(id, parameter);
    return new ApiResponseData<PostResult>(result);
  }

  [HttpDelete("{id}")]
  public async Task<ApiResponse> Delete(
    Guid id
  )
  {
    await _postService.Delete(id);

    return new ApiResponseData<PostResult>(null);
  }
}