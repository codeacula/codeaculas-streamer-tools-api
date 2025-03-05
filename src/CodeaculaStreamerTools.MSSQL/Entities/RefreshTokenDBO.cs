namespace CodeaculaStreamerTools.MSSQL.Entities;

using System.ComponentModel.DataAnnotations;

public class RefreshTokenDBO
{
  [Key]
  public Guid Id { get; set; } = Guid.NewGuid();

  public Guid UserId { get; set; }

  public DateTime ExpiresOn { get; set; } = DateTime.UtcNow.AddDays(7);

  public override string ToString() => Id.ToString();
}
