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
  public async Task<ApiResponse> GetAll()
  {
    var results = await _userService.GetAll();
    return new ApiResponseList<UserResult>(results);
  }

  [HttpGet("{id}")]
  public async Task<ApiResponse> FindOneById(Guid id)
  {
    var result = await _userService.FindOneById(id);
    return new ApiResponseData<UserResult>(result);
  }

  [HttpPost]
  public async Task<ApiResponse> Create(
    [FromBody] UserCreateParameter parameter
  )
  {
    var result = await _userService.Create(parameter);
    return new ApiResponseData<UserResult>(result, HttpStatusCode.Created);
  }

  [HttpPatch("{id}")]
  public async Task<ApiResponse> Update(
    Guid id,
    [FromBody] UserUpdateParameter parameter
  )
  {
    var result = await _userService.Update(id, parameter);
    return new ApiResponseData<UserResult>(result);
  }

  [HttpDelete("{id}")]
  public async Task<ApiResponse> Delete(
    Guid id
  )
  {
    await _userService.Delete(id);

    return new ApiResponseData<UserResult>(null);
  }
}