using dotnet_smk_telkom_2025.Infrastructure.Databases;
using dotnet_smk_telkom_2025.Models;

namespace dotnet_smk_telkom_2025.Repositories;

public class PostStoreRepository
{
  public SQLServerDBContext SQLServerDb { get; set; }

  public PostStoreRepository(
    SQLServerDBContext sqlServerDb
  )
  {
    SQLServerDb = sqlServerDb;
  }

  public async Task<Post> Create(Post post)
  {
    SQLServerDb.Posts.Add(post);
    await SQLServerDb.SaveChangesAsync();
    return post;
  }

  public async Task<Post> UpdateById(Guid id, Post post)
  {
    post.Id = id;
    SQLServerDb.Posts.Update(post);
    await SQLServerDb.SaveChangesAsync();
    return post;
  }

  public async Task Delete(Post existingData)
  {
    SQLServerDb.Posts.Remove(existingData);
    await SQLServerDb.SaveChangesAsync();
  }
}
