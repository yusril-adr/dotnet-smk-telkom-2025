namespace dotnet_smk_telkom_2025.Models;

public class Base
{
  public Guid Id { get; set; }
  public required DateTime CreatedAt { get; set; }
  public required DateTime UpdatedAt { get; set; }
  public DateTime? DeletedAt { get; set; }
}