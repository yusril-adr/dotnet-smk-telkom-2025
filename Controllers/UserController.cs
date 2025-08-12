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
    var results = _userService.GetAll();
    return Ok(results);
  }

  [HttpGet("{id}")]
  public IActionResult FindOneById(Guid id)
  {
    var results = _userService.FindOneById(id);
    return Ok(results);
  }

  [HttpPost]
  public IActionResult Create(
    [FromBody] UserCreateParameter parameter
  )
  {
    var result = _userService.Create(parameter);
    return Ok(result);
  }

  [HttpPatch("{id}")]
  public IActionResult Update(
    Guid id,
    [FromBody] UserUpdateParameter parameter
  )
  {
    var result = _userService.Update(id, parameter);
    return Ok(result);
  }

  [HttpDelete("{id}")]
  public IActionResult Delete(
    Guid id
  )
  {
    _userService.Delete(id);

    return Ok();
  }
}