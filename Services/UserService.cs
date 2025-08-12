using System.Net;
using dotnet_smk_telkom_2025.Dtos.Parameters;
using dotnet_smk_telkom_2025.Dtos.Results;
using dotnet_smk_telkom_2025.Infrastructure.Databases;
using dotnet_smk_telkom_2025.Infrastructure.Exceptions;
using dotnet_smk_telkom_2025.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_smk_telkom_2025.Services;

public class UserService
{
  private readonly UserQueryRepository _userQueryRepository;
  private readonly UserStoreRepository _userStoreRepository;
  public UserService(
    UserQueryRepository userQueryRepository,
    UserStoreRepository userStoreRepository
  )
  {
    _userQueryRepository = userQueryRepository;
    _userStoreRepository = userStoreRepository;
  }

  public (IActionResult, List<UserResult>) GetAll()
  {
    var users = _userQueryRepository.FindAll();
    var results = UserResult.MapModels(users);
    return (null, results);
  }

  public (IActionResult, UserResult) FindOneById(Guid id)
  {
    try
    {
      var user = _userQueryRepository.FindOneById(id);
      if (user == null)
      {
        throw new NotFoundException("User not found");
      }
      var result = new UserResult(user);
      return (null, result);
    }
    catch (NotFoundException e)
    {
      return (new NotFoundObjectResult(e.Message), null);
    }
    catch (Exception e)
    {
      return (new ObjectResult(e.Message) { StatusCode = StatusCodes.Status500InternalServerError }, null);
    }
  }

  public (IActionResult, UserResult) Create(
    UserCreateParameter parameter
  )
  {
    var user = UserCreateParameter.ToModel(parameter);
    user = _userStoreRepository.Create(user);

    var result = new UserResult(user);
    return (null, result);
  }

  public (IActionResult, UserResult) Update(
    Guid id,
    UserUpdateParameter parameter
  )
  {
    var user = _userQueryRepository.FindOneById(id);
    if (user == null)
    {
      return (new NotFoundObjectResult("User not found"), null);
    }
    user = UserUpdateParameter.ToModel(user, parameter);
    user = _userStoreRepository.UpdateById(id, user);

    var result = new UserResult(user);
    return (null, result);
  }

  public (IActionResult, UserResult) Delete(
    Guid id
  )
  {
    var user = _userQueryRepository.FindOneById(id);
    if (user == null)
    {
      return (new NotFoundObjectResult("User not found"), null);
    }

    _userStoreRepository.DeleteById(id);
    return (null, null);
  }
}
