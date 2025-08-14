using System.ComponentModel.DataAnnotations;
using dotnet_smk_telkom_2025.Models;

namespace dotnet_smk_telkom_2025.Dtos.Parameters;

public class PostUpdateParameter
{
  public string Title { get; set; }

  public string Content { get; set; }

  public static Post ToModel(Post post, PostUpdateParameter parameter)
  {
    post.Title = parameter.Title ?? post.Title;
    post.Content = parameter.Content ?? post.Content;
    return post;
  }
}