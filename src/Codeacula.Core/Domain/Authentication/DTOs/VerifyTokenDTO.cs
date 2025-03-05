namespace Codeacula.Core.Domain.Authentication.DTOs;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

public record VerifyTokenDTO
{
  [FromQuery(Name = "t")]
  [Required]
  public required string Token { get; set; }
}
