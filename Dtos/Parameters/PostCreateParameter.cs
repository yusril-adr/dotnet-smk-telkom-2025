using System.ComponentModel.DataAnnotations;
using dotnet_smk_telkom_2025.Models;

namespace dotnet_smk_telkom_2025.Dtos.Parameters;

public class PostCreateParameter
{
  [Required]
  public string Title { get; set; }

  [Required]
  public string Content { get; set; }

  public static Post ToModel(PostCreateParameter parameter, Guid userId)
  {
    return new Post
    {
      Title = parameter.Title,
      Content = parameter.Content,
      AuthorUserId = userId
    };
  }
}