using dotnet_smk_telkom_2025.Dtos.Parameters;
using dotnet_smk_telkom_2025.Dtos.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_smk_telkom_2025.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
  private readonly ILogger<UserController> _logger;

  public UserController(
    ILogger<UserController> logger
  )
  {
    _logger = logger;
  }

  [HttpGet]
  public IActionResult GetAll()
  {
    // TODO: Implement this method
    return Ok(new List<UserResult>());
  }

  [HttpGet("{id}")]
  public IActionResult FindOneById(Guid id)
  {
    // TODO: Implement this method
    return Ok();
  }

  [HttpPost]
  public IActionResult Create(
    [FromBody] UserCreateParameter parameter
  )
  {
    // TODO: Implement this method
    return Ok();
  }

  [HttpPut("{id}")]
  public IActionResult Update(
    Guid id,
    [FromBody] UserUpdateParameter parameter
  )
  {
    // TODO: Implement this method
    return Ok();
  }

  [HttpDelete("{id}")]
  public IActionResult Delete(
    Guid id
  )
  {
    // TODO: Implement this method
    return Ok();
  }
}