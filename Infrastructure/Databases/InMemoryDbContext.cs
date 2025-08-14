using dotnet_smk_telkom_2025.Models;

namespace dotnet_smk_telkom_2025.Infrastructure.Databases;

public class InMemoryDbContext
{
  public List<User> Users { get; set; } = [];

  public List<Post> Posts { get; set; } = [];

  public List<PostComment> PostComments { get; set; } = [];
}