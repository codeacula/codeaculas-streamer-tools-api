namespace Codeacula.Core.Domain.Authentication.Models;

using System.ComponentModel.DataAnnotations;

public record StateString
{
  [Key]
  public string State { get; set; } = Guid.NewGuid().ToString();

  public DateTime ExpiresOn { get; set; }
}
