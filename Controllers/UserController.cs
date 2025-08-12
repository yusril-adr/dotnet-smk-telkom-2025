using System.Net;
using dotnet_smk_telkom_2025.Dtos.Parameters;
using dotnet_smk_telkom_2025.Dtos.Results;
using dotnet_smk_telkom_2025.Infrastructure.Dtos;
using dotnet_smk_telkom_2025.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_smk_telkom_2025.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
  private readonly ILogger<UserController> _logger;
  private readonly UserService _userService;

  public UserController(
    ILogger<UserController> logger,
    UserService userService
  )
  {
    _logger = logger;
    _userService = userService;
  }

  [HttpGet]
  public ApiResponse GetAll()
  {
    var results = _userService.GetAll();
    return new ApiResponseList<UserResult>(results);
  }

  [HttpGet("{id}")]
  public ApiResponse FindOneById(Guid id)
  {
    var result = _userService.FindOneById(id);
    return new ApiResponseData<UserResult>(result);
  }

  [HttpPost]
  public ApiResponse Create(
    [FromBody] UserCreateParameter parameter
  )
  {
    var result = _userService.Create(parameter);
    return new ApiResponseData<UserResult>(result, HttpStatusCode.Created);
  }

  [HttpPatch("{id}")]
  public ApiResponse Update(
    Guid id,
    [FromBody] UserUpdateParameter parameter
  )
  {
    var result = _userService.Update(id, parameter);
    return new ApiResponseData<UserResult>(result);
  }

  [HttpDelete("{id}")]
  public ApiResponse Delete(
    Guid id
  )
  {
    _userService.Delete(id);

    return new ApiResponseData<UserResult>(null);
  }
}