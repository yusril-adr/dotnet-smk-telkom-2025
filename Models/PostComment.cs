namespace dotnet_smk_telkom_2025.Models;

public class PostComment : Base
{
  public required string Content { get; set; }

  public Guid AuthorUserId { get; set; }
  public Guid PostId { get; set; }

  /* ------------------------------- Relational ------------------------------- */
  public virtual required User AuthorUser { get; set; }

  public virtual required Post Post { get; set; }
}
