using dotnet_smk_telkom_2025.Infrastructure.Databases;
using dotnet_smk_telkom_2025.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_smk_telkom_2025.Repositories;

public class PostCommentQueryRepository
{
  public SQLServerDBContext SQLServerDb { get; set; }

  public PostCommentQueryRepository(
    SQLServerDBContext sqlServerDb
  )
  {
    SQLServerDb = sqlServerDb;
  }

  public async Task<List<PostComment>> FindAll()
  {
    return await SQLServerDb.PostComments.ToListAsync();
  }

  public async Task<PostComment> FindOneById(Guid id)
  {
    return await SQLServerDb.PostComments.FirstOrDefaultAsync(u => u.Id == id);
  }
}
