using dotnet_smk_telkom_2025.Dtos.Parameters;
using dotnet_smk_telkom_2025.Dtos.Results;
using dotnet_smk_telkom_2025.Infrastructure.Exceptions;
using dotnet_smk_telkom_2025.Repositories;

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

  public List<UserResult> GetAll()
  {
    var users = _userQueryRepository.FindAll();
    var results = UserResult.MapModels(users);
    return results;
  }

  public UserResult FindOneById(Guid id)
  {
    var user = _userQueryRepository.FindOneById(id);
    if (user == null)
    {
      throw new NotFoundException("User not found");
    }
    var result = new UserResult(user);
    return result;
  }

  public UserResult Create(
    UserCreateParameter parameter
  )
  {
    var user = UserCreateParameter.ToModel(parameter);
    user = _userStoreRepository.Create(user);

    var result = new UserResult(user);
    return result;
  }

  public UserResult Update(
    Guid id,
    UserUpdateParameter parameter
  )
  {
    var user = _userQueryRepository.FindOneById(id);
    if (user == null)
    {
      throw new NotFoundException("User not found");
    }
    user = UserUpdateParameter.ToModel(user, parameter);
    user = _userStoreRepository.UpdateById(id, user);

    var result = new UserResult(user);
    return result;
  }

  public void Delete(
    Guid id
  )
  {
    var user = _userQueryRepository.FindOneById(id);
    if (user == null)
    {
      throw new NotFoundException("User not found");
    }

    _userStoreRepository.DeleteById(id);
  }
}
