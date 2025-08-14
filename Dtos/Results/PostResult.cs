using dotnet_smk_telkom_2025.Models;

namespace dotnet_smk_telkom_2025.Dtos.Results;

public class PostResult
{
  public Guid Id { get; set; }
  public string Title { get; set; }
  public string Content { get; set; }
  public Guid AuthorUserId { get; set; }

  public UserResult AuthorUser { get; set; }

  public PostResult(
    Post post
  )
  {
    Id = post.Id;
    Title = post.Title;
    Content = post.Content;
    AuthorUserId = post.AuthorUserId;

    AuthorUser = post.AuthorUser != null ? new UserResult(post.AuthorUser) : null;
  }

  public static List<PostResult> MapModels(
    List<Post> posts
  )
  {
    return posts.Select(post => new PostResult(post)).ToList();
  }
}
