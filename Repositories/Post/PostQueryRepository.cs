using dotnet_smk_telkom_2025.Infrastructure.Databases;
using dotnet_smk_telkom_2025.Models;

namespace dotnet_smk_telkom_2025.Repositories;

public class PostQueryRepository
{
  public InMemoryDbContext InMemoryDb { get; set; }

  public PostQueryRepository(
    InMemoryDbContext inMemoryDb
  )
  {
    InMemoryDb = inMemoryDb;
  }

  public List<Post> FindAll()
  {
    return InMemoryDb.Posts;
  }

  public Post FindOneById(Guid id)
  {
    return InMemoryDb.Posts.FirstOrDefault(u => u.Id == id);
  }
}