using dotnet_smk_telkom_2025.Infrastructure.Databases;
using dotnet_smk_telkom_2025.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_smk_telkom_2025.Repositories;

public class PostQueryRepository
{
  public readonly SQLServerDBContext SQLServerDb;

  public PostQueryRepository(
    SQLServerDBContext sqlServerDb
  )
  {
    SQLServerDb = sqlServerDb;
  }

  public async Task<List<Post>> FindAll()
  {
    return await SQLServerDb.Posts.ToListAsync();
  }

  public async Task<Post> FindOneById(Guid id)
  {
    return await SQLServerDb.Posts.FirstOrDefaultAsync(u => u.Id == id);
  }
}