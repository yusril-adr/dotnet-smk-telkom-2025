using dotnet_smk_telkom_2025.Infrastructure.Databases;
using dotnet_smk_telkom_2025.Models;

namespace dotnet_smk_telkom_2025.Repositories;

public class PostCommentStoreRepository
{
  public SQLServerDBContext SQLServerDb { get; set; }

  public PostCommentStoreRepository(
    SQLServerDBContext sqlServerDb
  )
  {
    SQLServerDb = sqlServerDb;
  }

  public async Task<PostComment> Create(PostComment postComment)
  {
    SQLServerDb.PostComments.Add(postComment);
    await SQLServerDb.SaveChangesAsync();
    return postComment;
  }

  public async Task<PostComment> UpdateById(Guid id, PostComment postComment)
  {
    postComment.Id = id;
    SQLServerDb.PostComments.Update(postComment);
    await SQLServerDb.SaveChangesAsync();
    return postComment;
  }

  public async Task Delete(PostComment existingData)
  {
    SQLServerDb.PostComments.Remove(existingData);
    await SQLServerDb.SaveChangesAsync();
  }
}
