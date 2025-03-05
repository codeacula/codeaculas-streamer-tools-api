namespace Codeacula.MSSQL.Entities;

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

[Index(nameof(AggregateId))]
[Index(nameof(Timestamp))]
public abstract record EventSourceEventDBO
{
  [Key]
  public Guid Id { get; init; } = Guid.NewGuid();

  [Required]
  public required Guid AggregateId { get; init; }

  [Required]
  [MaxLength(200)]
  public required string EventType { get; init; }

  [Required]
  public required string Data { get; init; }

  public string? Metadata { get; init; }

  [Required]
  public DateTime Timestamp { get; init; } = DateTime.UtcNow;

  [Required]
  public int Version { get; init; } = 1;
}
