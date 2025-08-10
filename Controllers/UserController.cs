using dotnet_smk_telkom_2025.Dtos.Parameters;
using dotnet_smk_telkom_2025.Dtos.Results;
using dotnet_smk_telkom_2025.Models;
using dotnet_smk_telkom_2025.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_smk_telkom_2025.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
  private readonly ILogger<UserController> _logger;
  private readonly List<User> _userDatas;

  public UserController(
    ILogger<UserController> logger,
    UserQueryRepository userQueryRepository
  )
  {
    _logger = logger;
    _userDatas = userQueryRepository.GetDatas();
  }

  [HttpGet]
  public IActionResult GetAll()
  {
    var results = UserResult.MapModels(_userDatas);
    return Ok(results);
  }

  [HttpGet("{id}")]
  public IActionResult FindOneById(Guid id)
  {
    var user = _userDatas.FirstOrDefault(u => u.Id == id);
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
    _userDatas.Add(user);

    var result = new UserResult(user);
    return Ok(result);
  }

  [HttpPatch("{id}")]
  public IActionResult Update(
    Guid id,
    [FromBody] UserUpdateParameter parameter
  )
  {
    var user = _userDatas.FirstOrDefault(u => u.Id == id);
    if (user == null)
    {
      return NotFound("User not found");
    }
    user = UserUpdateParameter.ToModel(user, parameter);

    var index = _userDatas.FindIndex(u => u.Id == user.Id);
    if (index >= 0)
    {
      user.UpdatedAt = DateTime.Now;
      _userDatas[index] = user;
    }

    var result = new UserResult(user);
    return Ok(result);
  }

  [HttpDelete("{id}")]
  public IActionResult Delete(
    Guid id
  )
  {
    var user = _userDatas.FirstOrDefault(u => u.Id == id);
    if (user == null)
    {
      return NotFound("User not found");
    }
    _userDatas.Remove(user);
    return Ok();
  }
}