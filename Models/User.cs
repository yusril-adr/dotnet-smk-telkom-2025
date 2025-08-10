namespace dotnet_smk_telkom_2025.Models;

public class User : Base
{
  public string Name { get; set; }
  public string Email { get; set; }

  /* ------------------------------- Relational ------------------------------- */
  public virtual ICollection<Post> Posts { get; set; }

  public virtual ICollection<PostComment> PostComments { get; set; }
}