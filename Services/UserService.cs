using dotnet_smk_telkom_2025.Dtos.Parameters;
using dotnet_smk_telkom_2025.Dtos.Results;
using dotnet_smk_telkom_2025.Infrastructure.Databases;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_smk_telkom_2025.Services;

public class UserService
{
  private readonly InMemoryDbContext _inMemoryDb;

  public UserService(
    InMemoryDbContext inMemoryDb
  )
  {
    _inMemoryDb = inMemoryDb;
  }

  public (IActionResult, List<UserResult>) GetAll()
  {
    var results = UserResult.MapModels(_inMemoryDb.Users);
    return (null, results);
  }

  public (IActionResult, UserResult) FindOneById(Guid id)
  {
    var user = _inMemoryDb.Users.FirstOrDefault(u => u.Id == id);
    if (user == null)
    {
      return (new NotFoundObjectResult("User not found"), null);
    }
    var result = new UserResult(user);
    return (null, result);
  }

  public (IActionResult, UserResult) Create(
    UserCreateParameter parameter
  )
  {
    var user = UserCreateParameter.ToModel(parameter);
    user.Id = Guid.NewGuid();
    user.CreatedAt = DateTime.Now;
    user.UpdatedAt = DateTime.Now;
    _inMemoryDb.Users.Add(user);

    var result = new UserResult(user);
    return (null, result);
  }

  public (IActionResult, UserResult) Update(
    Guid id,
    UserUpdateParameter parameter
  )
  {
    var user = _inMemoryDb.Users.FirstOrDefault(u => u.Id == id);
    if (user == null)
    {
      return (new NotFoundObjectResult("User not found"), null);
    }
    user = UserUpdateParameter.ToModel(user, parameter);

    var index = _inMemoryDb.Users.FindIndex(u => u.Id == user.Id);
    if (index >= 0)
    {
      user.UpdatedAt = DateTime.Now;
      _inMemoryDb.Users[index] = user;
    }

    var result = new UserResult(user);
    return (null, result);
  }

  public (IActionResult, UserResult) Delete(
    Guid id
  )
  {
    var user = _inMemoryDb.Users.FirstOrDefault(u => u.Id == id);
    if (user == null)
    {
      return (new NotFoundObjectResult("User not found"), null);
    }
    _inMemoryDb.Users.Remove(user);
    return (null, null);
  }
}
