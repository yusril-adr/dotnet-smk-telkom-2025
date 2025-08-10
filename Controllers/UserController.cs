using dotnet_smk_telkom_2025.Dtos.Parameters;
using dotnet_smk_telkom_2025.Dtos.Results;
using dotnet_smk_telkom_2025.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_smk_telkom_2025.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
  private readonly ILogger<UserController> _logger;

  private List<User> UserDatas = new();

  public UserController(
    ILogger<UserController> logger
  )
  {
    _logger = logger;
  }

  [HttpGet]
  public IActionResult GetAll()
  {
    var results = UserResult.MapModels(UserDatas);
    return Ok(results);
  }

  [HttpGet("{id}")]
  public IActionResult FindOneById(Guid id)
  {
    var user = UserDatas.FirstOrDefault(u => u.Id == id);
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
    UserDatas.Add(user);

    return Ok(user);
  }

  [HttpPatch("{id}")]
  public IActionResult Update(
    Guid id,
    [FromBody] UserUpdateParameter parameter
  )
  {
    var user = UserDatas.FirstOrDefault(u => u.Id == id);
    if (user == null)
    {
      return NotFound("User not found");
    }

    UserUpdateParameter.ToModel(user, parameter);
    user.UpdatedAt = DateTime.Now;

    var index = UserDatas.FindIndex(u => u.Id == user.Id);
    if (index >= 0)
    {
      user.UpdatedAt = DateTime.Now;
      UserDatas[index] = user;
    }

    var result = new UserResult(user);
    return Ok(result);
  }

  [HttpDelete("{id}")]
  public IActionResult Delete(
    Guid id
  )
  {
    var user = UserDatas.FirstOrDefault(u => u.Id == id);
    if (user == null)
    {
      return NotFound("User not found");
    }
    UserDatas.Remove(user);
    return Ok();
  }
}