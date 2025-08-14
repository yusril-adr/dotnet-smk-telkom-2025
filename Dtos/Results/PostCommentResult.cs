using dotnet_smk_telkom_2025.Models;

namespace dotnet_smk_telkom_2025.Dtos.Results;

public class PostCommentResult
{
  public Guid Id { get; set; }
  public string Content { get; set; }
  public Guid AuthorUserId { get; set; }
  public Guid PostId { get; set; }

  public UserResult AuthorUser { get; set; }
  public PostResult Post { get; set; }

  public PostCommentResult(
    PostComment postComment
  )
  {
    Id = postComment.Id;
    Content = postComment.Content;
    AuthorUserId = postComment.AuthorUserId;
    PostId = postComment.PostId;

    AuthorUser = postComment.AuthorUser != null ? new UserResult(postComment.AuthorUser) : null;
    Post = postComment.Post != null ? new PostResult(postComment.Post) : null;
  }

  public static List<PostCommentResult> MapModels(
    List<PostComment> postComments
  )
  {
    return postComments.Select(postComment => new PostCommentResult(postComment)).ToList();
  }
}
