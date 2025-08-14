using dotnet_smk_telkom_2025.Infrastructure.Databases;
using dotnet_smk_telkom_2025.Models;

namespace dotnet_smk_telkom_2025.Repositories;

public class PostCommentStoreRepository
{
  public InMemoryDbContext InMemoryDb { get; set; }

  public PostCommentStoreRepository(
    InMemoryDbContext inMemoryDb
  )
  {
    InMemoryDb = inMemoryDb;
  }

  public PostComment Create(PostComment postComment)
  {
    postComment.Id = Guid.NewGuid();
    postComment.CreatedAt = DateTime.Now;
    postComment.UpdatedAt = DateTime.Now;
    InMemoryDb.PostComments.Add(postComment);
    return postComment;
  }

  public PostComment UpdateById(Guid id, PostComment postComment)
  {
    var index = InMemoryDb.PostComments.FindIndex(u => u.Id == id);
    if (index >= 0)
    {
      postComment.UpdatedAt = DateTime.Now;
      InMemoryDb.PostComments[index] = postComment;
    }
    return postComment;
  }

  public void DeleteById(Guid id)
  {
    var postComment = InMemoryDb.PostComments.FirstOrDefault(u => u.Id == id);
    if (postComment == null)
    {
      return;
    }
    InMemoryDb.PostComments.Remove(postComment);
  }
}
