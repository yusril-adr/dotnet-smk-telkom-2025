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

  public async Task<List<UserResult>> GetAll()
  {
    var users = await _userQueryRepository.FindAll();
    var results = UserResult.MapModels(users);
    return results;
  }

  public async Task<UserResult> FindOneById(Guid id)
  {
    var user = await _userQueryRepository.FindOneById(id);
    if (user == null)
    {
      throw new NotFoundException("User not found");
    }
    var result = new UserResult(user);
    return result;
  }

  public async Task<UserResult> Create(
    UserCreateParameter parameter
  )
  {
    var user = UserCreateParameter.ToModel(parameter);
    user = await _userStoreRepository.Create(user);

    var result = new UserResult(user);
    return result;
  }

  public async Task<UserResult> Update(
    Guid id,
    UserUpdateParameter parameter
  )
  {
    var user = await _userQueryRepository.FindOneById(id);
    if (user == null)
    {
      throw new NotFoundException("User not found");
    }
    user = UserUpdateParameter.ToModel(user, parameter);
    user = await _userStoreRepository.UpdateById(id, user);

    var result = new UserResult(user);
    return result;
  }

  public async Task Delete(
    Guid id
  )
  {
    var user = await _userQueryRepository.FindOneById(id);
    if (user == null)
    {
      throw new NotFoundException("User not found");
    }

    await _userStoreRepository.Delete(user);
  }
}
