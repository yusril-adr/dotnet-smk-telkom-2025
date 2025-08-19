using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using dotnet_smk_telkom_2025.Dtos.Parameters;
using dotnet_smk_telkom_2025.Dtos.Results;
using dotnet_smk_telkom_2025.Infrastructure.Dtos;
using dotnet_smk_telkom_2025.Infrastructure.Shared;
using dotnet_smk_telkom_2025.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_smk_telkom_2025.Controllers;

[ApiController]
[Route("posts")]
public class PostController : ControllerBase
{
  private readonly ILogger<PostController> _logger;
  private readonly PostService _postService;
  private readonly AuthUtil _authUtil;
  private readonly IHttpContextAccessor _httpContextAccessor;

  public PostController(
    ILogger<PostController> logger,
    PostService postService,
    AuthUtil authUtil,
    IHttpContextAccessor httpContextAccessor
  )
  {
    _logger = logger;
    _postService = postService;
    _authUtil = authUtil;
    _httpContextAccessor = httpContextAccessor;
  }

  [HttpGet]
  [AllowAnonymous]
  public async Task<ApiResponse> Pagination(
    [FromQuery] PostQueryParameter parameter
  )
  {
    var results = await _postService.Pagination(parameter);
    Console.WriteLine(JsonSerializer.Serialize(results));
    return new ApiResponsePagination<PostResult>(results);
  }

  [HttpGet("{id}")]
  [AllowAnonymous]
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
    var loggedUserId = _authUtil.GetUserLoggedId(_httpContextAccessor.HttpContext);
    var result = await _postService.Create(parameter, loggedUserId);
    return new ApiResponseData<PostResult>(result, HttpStatusCode.Created);
  }

  [HttpPatch("{id}")]
  public async Task<ApiResponse> Update(
    Guid id,
    [FromBody] PostUpdateParameter parameter
  )
  {
    var loggedUserId = _authUtil.GetUserLoggedId(_httpContextAccessor.HttpContext);
    var result = await _postService.Update(id, parameter, loggedUserId);
    return new ApiResponseData<PostResult>(result);
  }

  [HttpDelete("{id}")]
  public async Task<ApiResponse> Delete(
    Guid id
  )
  {
    var loggedUserId = _authUtil.GetUserLoggedId(_httpContextAccessor.HttpContext);

    await _postService.Delete(id, loggedUserId);

    return new ApiResponseData<PostResult>(null);
  }
}