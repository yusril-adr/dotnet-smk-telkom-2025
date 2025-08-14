using dotnet_smk_telkom_2025.Infrastructure.Databases;
using dotnet_smk_telkom_2025.Models;

namespace dotnet_smk_telkom_2025.Repositories;

public class PostStoreRepository
{
  public InMemoryDbContext InMemoryDb { get; set; }

  public PostStoreRepository(
    InMemoryDbContext inMemoryDb
  )
  {
    InMemoryDb = inMemoryDb;
  }

  public Post Create(Post post)
  {
    post.Id = Guid.NewGuid();
    post.CreatedAt = DateTime.Now;
    post.UpdatedAt = DateTime.Now;
    InMemoryDb.Posts.Add(post);
    return post;
  }

  public Post UpdateById(Guid id, Post post)
  {
    var index = InMemoryDb.Posts.FindIndex(u => u.Id == id);
    if (index >= 0)
    {
      post.UpdatedAt = DateTime.Now;
      InMemoryDb.Posts[index] = post;
    }
    return post;
  }

  public void DeleteById(Guid id)
  {
    var post = InMemoryDb.Posts.FirstOrDefault(u => u.Id == id);
    if (post == null)
    {
      return;
    }
    InMemoryDb.Posts.Remove(post);
  }
}
