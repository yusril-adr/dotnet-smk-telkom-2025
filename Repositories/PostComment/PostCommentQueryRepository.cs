using dotnet_smk_telkom_2025.Infrastructure.Databases;
using dotnet_smk_telkom_2025.Models;

namespace dotnet_smk_telkom_2025.Repositories;

public class PostCommentQueryRepository
{
  public InMemoryDbContext InMemoryDb { get; set; }

  public PostCommentQueryRepository(
    InMemoryDbContext inMemoryDb
  )
  {
    InMemoryDb = inMemoryDb;
  }

  public List<PostComment> FindAll()
  {
    return InMemoryDb.PostComments;
  }

  public PostComment FindOneById(Guid id)
  {
    return InMemoryDb.PostComments.FirstOrDefault(u => u.Id == id);
  }
}
