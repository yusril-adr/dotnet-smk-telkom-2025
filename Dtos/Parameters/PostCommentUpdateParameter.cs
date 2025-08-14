using System.ComponentModel.DataAnnotations;
using dotnet_smk_telkom_2025.Models;

namespace dotnet_smk_telkom_2025.Dtos.Parameters;

public class PostCommentUpdateParameter
{
  public string Content { get; set; }

  public static PostComment ToModel(PostComment postComment, PostCommentUpdateParameter parameter)
  {
    postComment.Content = parameter.Content ?? postComment.Content;
    return postComment;
  }
}