namespace CodeaculaStreamerTools.Core.Domain.Users.Models;

public class User
{
  public Guid Id { get; set; }

  public required string DisplayName { get; set; }

  public required string Email { get; set; }

  public required string ProfileImageUrl { get; set; }

  public required string Username { get; set; }

  public int OrbEssence { get; set; }

  public ICollection<Role> Roles { get; set; } = [];
}
