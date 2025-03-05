namespace Codeacula.Core.Domain.Twitch.DTOs;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

public record TwitchAuthRequestDTO
{
  [FromQuery(Name = "code")]
  [Required]
  public required string Code { get; init; }

  [FromQuery(Name = "state")]
  [Required]
  public required string State { get; init; }
}
