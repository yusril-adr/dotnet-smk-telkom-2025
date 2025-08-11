using dotnet_smk_telkom_2025.Dtos.Parameters;
using dotnet_smk_telkom_2025.Dtos.Results;
using dotnet_smk_telkom_2025.Infrastructure.Databases;
using dotnet_smk_telkom_2025.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_smk_telkom_2025.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
  private readonly ILogger<UserController> _logger;
  private readonly InMemoryDbContext _inMemoryDb;

  public UserController(
    ILogger<UserController> logger,
    InMemoryDbContext inMemoryDb
  )
  {
    _logger = logger;
    _inMemoryDb = inMemoryDb;
  }

  [HttpGet]
  public IActionResult GetAll()
  {
    var results = UserResult.MapModels(_inMemoryDb.Users);
    return Ok(results);
  }

  [HttpGet("{id}")]
  public IActionResult FindOneById(Guid id)
  {
    var user = _inMemoryDb.Users.FirstOrDefault(u => u.Id == id);
    if (user == null)
    {
      return NotFound("User not found");
    }
    var result = new UserResult(user);
    return Ok(result);
  }

  [HttpPost]
  public IActionResult Create(
    [FromBody] UserCreateParameter parameter
  )
  {
    var user = UserCreateParameter.ToModel(parameter);
    user.Id = Guid.NewGuid();
    user.CreatedAt = DateTime.Now;
    user.UpdatedAt = DateTime.Now;
    _inMemoryDb.Users.Add(user);

    var result = new UserResult(user);
    return Ok(result);
  }

  [HttpPatch("{id}")]
  public IActionResult Update(
    Guid id,
    [FromBody] UserUpdateParameter parameter
  )
  {
    var user = _inMemoryDb.Users.FirstOrDefault(u => u.Id == id);
    if (user == null)
    {
      return NotFound("User not found");
    }
    user = UserUpdateParameter.ToModel(user, parameter);

    var index = _inMemoryDb.Users.FindIndex(u => u.Id == user.Id);
    if (index >= 0)
    {
      user.UpdatedAt = DateTime.Now;
      _inMemoryDb.Users[index] = user;
    }

    var result = new UserResult(user);
    return Ok(result);
  }

  [HttpDelete("{id}")]
  public IActionResult Delete(
    Guid id
  )
  {
    var user = _inMemoryDb.Users.FirstOrDefault(u => u.Id == id);
    if (user == null)
    {
      return NotFound("User not found");
    }
    _inMemoryDb.Users.Remove(user);
    return Ok();
  }
}