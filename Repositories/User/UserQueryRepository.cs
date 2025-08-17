using dotnet_smk_telkom_2025.Infrastructure.Databases;
using dotnet_smk_telkom_2025.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_smk_telkom_2025.Repositories;

public class UserQueryRepository
{
  public readonly SQLServerDBContext SQLServerDB;

  public UserQueryRepository(
    SQLServerDBContext sqlServerDb
  )
  {
    SQLServerDB = sqlServerDb;
  }

  public async Task<List<User>> FindAll()
  {
    return await SQLServerDB.Users.ToListAsync();
  }

  public async Task<User> FindOneById(Guid id)
  {
    return await SQLServerDB.Users.FirstOrDefaultAsync(u => u.Id == id);
  }
}
