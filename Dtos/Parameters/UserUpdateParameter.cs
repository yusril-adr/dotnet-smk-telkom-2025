using System.ComponentModel.DataAnnotations;
using dotnet_smk_telkom_2025.Models;

namespace dotnet_smk_telkom_2025.Dtos.Parameters;

public class UserUpdateParameter : UserCreateParameter
{
  [StringLength(100, MinimumLength = 3)]
  public new string Name { get; set; }
  public static User ToModel(User user, UserUpdateParameter parameter)
  {
    user.Name = parameter.Name ?? user.Name;
    user.Email = parameter.Email ?? user.Email;
    return user;
  }
}
