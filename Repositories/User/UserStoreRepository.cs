using dotnet_smk_telkom_2025.Infrastructure.Databases;
using dotnet_smk_telkom_2025.Models;

namespace dotnet_smk_telkom_2025.Repositories;

public class UserStoreRepository
{
  public InMemoryDbContext InMemoryDb { get; set; }

  public UserStoreRepository(
    InMemoryDbContext inMemoryDb
  )
  {
    InMemoryDb = inMemoryDb;
  }

  public User Create(User user)
  {
    user.Id = Guid.NewGuid();
    user.CreatedAt = DateTime.Now;
    user.UpdatedAt = DateTime.Now;
    InMemoryDb.Users.Add(user);
    return user;
  }

  public User UpdateById(Guid id, User user)
  {
    var index = InMemoryDb.Users.FindIndex(u => u.Id == id);
    if (index >= 0)
    {
      user.UpdatedAt = DateTime.Now;
      InMemoryDb.Users[index] = user;
    }
    return user;
  }

  public void DeleteById(Guid id)
  {
    var user = InMemoryDb.Users.FirstOrDefault(u => u.Id == id);
    if (user == null)
    {
      return;
    }
    InMemoryDb.Users.Remove(user);
  }
}
