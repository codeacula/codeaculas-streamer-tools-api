namespace Codeacula.MSSQL.Entities;

using System.ComponentModel.DataAnnotations;

public class AccessCredentialDBO
{
  [Key]
  public int Id { get; set; }

  public Guid UserId { get; set; }

  public required int ServiceType { get; set; }

  public required int TokenType { get; set; }

  public required string TokenValue { get; set; }
}
