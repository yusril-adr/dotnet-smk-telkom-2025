using dotnet_smk_telkom_2025.Infrastructure.Databases;
using dotnet_smk_telkom_2025.Models;

namespace dotnet_smk_telkom_2025.Repositories;

public class UserQueryRepository
{
  public InMemoryDbContext InMemoryDb { get; set; }

  public UserQueryRepository(
    InMemoryDbContext inMemoryDb
  )
  {
    InMemoryDb = inMemoryDb;
  }

  public List<User> FindAll()
  {
    return InMemoryDb.Users;
  }

  public User FindOneById(Guid id)
  {
    return InMemoryDb.Users.FirstOrDefault(u => u.Id == id);
  }
}
