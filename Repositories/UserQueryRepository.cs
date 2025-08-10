using dotnet_smk_telkom_2025.Models;

namespace dotnet_smk_telkom_2025.Repositories;

public class UserQueryRepository
{
  private List<User> UserDatas = [];

  public List<User> GetDatas()
  {
    return UserDatas;
  }
}
