using dotnet_smk_telkom_2025.Models;

namespace dotnet_smk_telkom_2025.Dtos.Results;

public class UserResult
{
  public Guid Id { get; set; }
  public string Name { get; set; }
  public string Email { get; set; }

  public UserResult(
    User user
  )
  {
    Id = user.Id;
    Name = user.Name;
    Email = user.Email;
  }

  public static List<UserResult> MapModels(
    List<User> users
  )
  {
    return users.Select(user => new UserResult(user)).ToList();
  }
}
