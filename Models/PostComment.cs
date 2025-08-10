namespace dotnet_smk_telkom_2025.Models;

public class PostComment : Base
{
  public string Content { get; set; }

  public Guid AuthorUserId { get; set; }
  public Guid PostId { get; set; }

  /* ------------------------------- Relational ------------------------------- */
  public virtual User AuthorUser { get; set; }

  public virtual Post Post { get; set; }
}
