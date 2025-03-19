namespace CodeaculaStreamerTools.Core.Common.ES;

using System.ComponentModel.DataAnnotations;

public record IncomingEvent
{
  [Required]
  public required Guid AggregateId { get; set; }

  [Required]
  [MaxLength(100)]
  public required string AggregateType { get; set; }

  [Required]
  [MaxLength(200)]
  public required string EventType { get; set; }

  [Required]
  public required string Data { get; set; }

  public string? Metadata { get; set; }

  [Required]
  public required int Version { get; set; }
}
