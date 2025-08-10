namespace dotnet_smk_telkom_2025.Models;

public class Post : Base
{
  public required string Title { get; set; }
  public required string Content { get; set; }
  public Guid AuthorUserId { get; set; }

  /* ------------------------------- Relational ------------------------------- */
  public virtual required User AuthorUser { get; set; }
  public virtual ICollection<PostComment> Comments { get; set; }
}