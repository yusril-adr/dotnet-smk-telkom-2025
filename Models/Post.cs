namespace dotnet_smk_telkom_2025.Models;

public class Post : Base
{
  public string Title { get; set; }
  public string Content { get; set; }
  public Guid AuthorUserId { get; set; }

  /* ------------------------------- Relational ------------------------------- */
  public virtual User AuthorUser { get; set; }
  public virtual ICollection<PostComment> Comments { get; set; }
}