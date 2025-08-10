using System.ComponentModel.DataAnnotations;

namespace dotnet_smk_telkom_2025.Dtos.Parameters;

public class UserCreateParameter
{
  [Required]
  [StringLength(100, MinimumLength = 3)]
  public string Name { get; set; }

  [EmailAddress]
  public string Email { get; set; }
}