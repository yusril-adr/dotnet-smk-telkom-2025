using System.ComponentModel.DataAnnotations;
using dotnet_smk_telkom_2025.Models;

namespace dotnet_smk_telkom_2025.Dtos.Parameters;

public class PostCommentCreateParameter
{
  [Required]
  public string Content { get; set; }


  [Required]
  public Guid PostId { get; set; }

  public static PostComment ToModel(PostCommentCreateParameter parameter, Guid userId)
  {
    return new PostComment
    {
      Content = parameter.Content,
      AuthorUserId = userId,
      PostId = parameter.PostId
    };
  }
}