using dotnet_smk_telkom_2025.Dtos.Parameters;
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
  public IActionResult GetAll()
  {
    var (error, results) = _userService.GetAll();
    if (error != null)
    {
      return error;
    }

    return Ok(results);
  }

  [HttpGet("{id}")]
  public IActionResult FindOneById(Guid id)
  {
    var (error, results) = _userService.FindOneById(id);
    if (error != null)
    {
      return error;
    }

    return Ok(results);
  }

  [HttpPost]
  public IActionResult Create(
    [FromBody] UserCreateParameter parameter
  )
  {
    var (error, results) = _userService.Create(parameter);
    if (error != null)
    {
      return error;
    }

    return Ok(results);
  }

  [HttpPatch("{id}")]
  public IActionResult Update(
    Guid id,
    [FromBody] UserUpdateParameter parameter
  )
  {
    var (error, results) = _userService.Update(id, parameter);
    if (error != null)
    {
      return error;
    }

    return Ok(results);
  }

  [HttpDelete("{id}")]
  public IActionResult Delete(
    Guid id
  )
  {
    var (error, results) = _userService.Delete(id);
    if (error != null)
    {
      return error;
    }

    return Ok(results);
  }
}