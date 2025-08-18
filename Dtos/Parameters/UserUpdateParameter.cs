using BC = BCrypt.Net.BCrypt;
using System.ComponentModel.DataAnnotations;
using dotnet_smk_telkom_2025.Models;

namespace dotnet_smk_telkom_2025.Dtos.Parameters;

public class UserUpdateParameter
{
  [StringLength(100, MinimumLength = 3)]
  public string Name { get; set; }

  [EmailAddress]
  public string Email { get; set; }

  public string Password { get; set; }

  public static User ToModel(User user, UserUpdateParameter parameter)
  {
    user.Name = parameter.Name ?? user.Name;
    user.Email = parameter.Email ?? user.Email;
    user.Password = BC.HashPassword(parameter.Password);
    return user;
  }
}
