using dotnet_smk_telkom_2025.Infrastructure.Databases;
using dotnet_smk_telkom_2025.Models;

namespace dotnet_smk_telkom_2025.Repositories;

public class UserStoreRepository
{
  public SQLServerDBContext SQLServerDb;

  public UserStoreRepository(
    SQLServerDBContext sqlServerDb
  )
  {
    SQLServerDb = sqlServerDb;
  }

  public async Task<User> Create(User user)
  {
    SQLServerDb.Users.Add(user);
    await SQLServerDb.SaveChangesAsync();
    return user;
  }

  public async Task<User> UpdateById(Guid id, User user)
  {
    user.Id = id;
    SQLServerDb.Users.Update(user);
    await SQLServerDb.SaveChangesAsync();
    return user;
  }

  public async Task Delete(User existingData)
  {
    SQLServerDb.Users.Remove(existingData);
    await SQLServerDb.SaveChangesAsync();
  }
}
