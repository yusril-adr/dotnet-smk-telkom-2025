using dotnet_smk_telkom_2025.Infrastructure.Dtos;

namespace dotnet_smk_telkom_2025.Dtos.Parameters;

public class PostCommentQueryParameter : QueryParameter
{
  public Guid PostId { get; set; }
  public Guid AuthorUserId { get; set; }
}
