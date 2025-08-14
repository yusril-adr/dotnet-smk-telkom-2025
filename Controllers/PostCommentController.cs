using System.Net;
using dotnet_smk_telkom_2025.Dtos.Parameters;
using dotnet_smk_telkom_2025.Dtos.Results;
using dotnet_smk_telkom_2025.Infrastructure.Dtos;
using dotnet_smk_telkom_2025.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_smk_telkom_2025.Controllers;

[ApiController]
[Route("posts/comments")]
public class PostCommentController : ControllerBase
{
  private readonly ILogger<PostCommentController> _logger;
  private readonly PostCommentService _postCommentService;

  public PostCommentController(
    ILogger<PostCommentController> logger,
    PostCommentService postCommentService
  )
  {
    _logger = logger;
    _postCommentService = postCommentService;
  }

  [HttpGet]
  public ApiResponse GetAll()
  {
    var results = _postCommentService.GetAll();
    return new ApiResponseList<PostCommentResult>(results);
  }

  [HttpGet("{id}")]
  public ApiResponse FindOneById(Guid id)
  {
    var result = _postCommentService.FindOneById(id);
    return new ApiResponseData<PostCommentResult>(result);
  }

  [HttpPost]
  public ApiResponse Create(
    [FromBody] PostCommentCreateParameter parameter
  )
  {
    var result = _postCommentService.Create(parameter);
    return new ApiResponseData<PostCommentResult>(result, HttpStatusCode.Created);
  }

  [HttpPatch("{id}")]
  public ApiResponse Update(
    Guid id,
    [FromBody] PostCommentUpdateParameter parameter
  )
  {
    var result = _postCommentService.Update(id, parameter);
    return new ApiResponseData<PostCommentResult>(result);
  }

  [HttpDelete("{id}")]
  public ApiResponse Delete(
    Guid id
  )
  {
    _postCommentService.Delete(id);

    return new ApiResponseData<PostCommentResult>(null);
  }
}