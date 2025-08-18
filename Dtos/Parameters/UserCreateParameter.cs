using BC = BCrypt.Net.BCrypt;
using System.ComponentModel.DataAnnotations;
using dotnet_smk_telkom_2025.Models;

namespace dotnet_smk_telkom_2025.Dtos.Parameters;

public class UserCreateParameter
{
  [Required]
  [StringLength(100, MinimumLength = 3)]
  public string Name { get; set; }

  [Required]
  [EmailAddress]
  public string Email { get; set; }

  [Required]
  public string Password { get; set; }

  public static User ToModel(UserCreateParameter parameter)
  {
    return new User
    {
      Name = parameter.Name,
      Email = parameter.Email,
      Password = BC.HashPassword(parameter.Password)
    };
  }
}