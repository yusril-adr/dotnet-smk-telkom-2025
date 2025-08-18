using System.Net;
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
[Route("posts/comments")]
public class PostCommentController : ControllerBase
{
  private readonly ILogger<PostCommentController> _logger;
  private readonly PostCommentService _postCommentService;
  private readonly AuthUtil _authUtil;
  private readonly IHttpContextAccessor _httpContextAccessor;


  public PostCommentController(
    ILogger<PostCommentController> logger,
    PostCommentService postCommentService
  )
  {
    _logger = logger;
    _postCommentService = postCommentService;
  }

  [HttpGet]
  [AllowAnonymous]
  public async Task<ApiResponse> GetAll()
  {
    var results = await _postCommentService.GetAll();
    return new ApiResponseList<PostCommentResult>(results);
  }

  [HttpGet("{id}")]
  [AllowAnonymous]
  public async Task<ApiResponse> FindOneById(Guid id)
  {
    var result = await _postCommentService.FindOneById(id);
    return new ApiResponseData<PostCommentResult>(result);
  }

  [HttpPost]
  public async Task<ApiResponse> Create(
    [FromBody] PostCommentCreateParameter parameter
  )
  {
    var loggedUserId = _authUtil.GetUserLoggedId(_httpContextAccessor.HttpContext);
    var result = await _postCommentService.Create(parameter, loggedUserId);
    return new ApiResponseData<PostCommentResult>(result, HttpStatusCode.Created);
  }

  [HttpPatch("{id}")]
  public async Task<ApiResponse> Update(
    Guid id,
    [FromBody] PostCommentUpdateParameter parameter
  )
  {
    var loggedUserId = _authUtil.GetUserLoggedId(_httpContextAccessor.HttpContext);
    var result = await _postCommentService.Update(id, parameter, loggedUserId);
    return new ApiResponseData<PostCommentResult>(result);
  }

  [HttpDelete("{id}")]
  public async Task<ApiResponse> Delete(
    Guid id
  )
  {
    var loggedUserId = _authUtil.GetUserLoggedId(_httpContextAccessor.HttpContext);
    await _postCommentService.Delete(id, loggedUserId);

    return new ApiResponseData<PostCommentResult>(null);
  }
}