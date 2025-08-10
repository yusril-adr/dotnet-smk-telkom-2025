using System.ComponentModel.DataAnnotations;
using dotnet_smk_telkom_2025.Models;

namespace dotnet_smk_telkom_2025.Dtos.Parameters;

public class UserCreateParameter
{
  [Required]
  [StringLength(100, MinimumLength = 3)]
  public string Name { get; set; }

  [EmailAddress]
  public string Email { get; set; }

  public static User ToModel(UserCreateParameter parameter)
  {
    return new User
    {
      Name = parameter.Name,
      Email = parameter.Email
    };
  }
}