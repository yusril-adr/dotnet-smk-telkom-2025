using System.ComponentModel.DataAnnotations;

namespace dotnet_smk_telkom_2025.Dtos.Parameters;

public class AuthLoginParameter
{
  [Required]
  public string Email { get; set; }

  [Required]
  public string Password { get; set; }
}
