namespace Codeacula.API.Controllers.V1;

using Codeacula.API.Infrastructure.JWT;
using Codeacula.Core.Common.Services;
using Codeacula.Core.Domain.Authentication.Commands;
using Codeacula.Core.Domain.Authentication.DTOs;
using Codeacula.Core.Domain.Authentication.Queries;
using Codeacula.Core.Domain.Twitch.DTOs;
using Microsoft.AspNetCore.Mvc;

public sealed class AuthController(
  IJwtService jwtService,
  ILogger<AuthController> logger,
  IMediatorService mediatorService) : BaseController<AuthController>(logger, mediatorService)
{
  private readonly IJwtService jwtService = jwtService;

  /// <summary>
  /// Allows a client to get a URL to redirect to for Twitch authentication.
  /// </summary>
  /// <returns>The URL the client should use to authenticate via Twitch.</returns>
  [HttpGet]
  [Route("twitch")]
  public Task<IActionResult> GetTwitchUrlAsync() =>
    this.HandleResultAsync(() => Mediator.ExecuteQueryAsync(new GetTwitchUrlQuery()));

  /// <summary>
  /// Completes the Twitch authentication flow with the provided code.
  /// </summary>
  /// <param name="request">The authentication request containing the code and state.</param>
  /// <returns>The authentication result containing access and refresh tokens.</returns>
  [HttpPost]
  [Route("twitch")]
  public async Task<IActionResult> TwitchAuthAsync([FromBody] TwitchAuthRequestDTO request)
  {
    ArgumentNullException.ThrowIfNull(request);

    var result = await Mediator.ExecuteCommandAsync(new LoginViaTwitchCommand(request.Code, request.State));

    if (result.IsError)
    {
      return Bad(result.ErrorMessage);
    }

    Response.Cookies.Append("refresh_token", result.Value.RefreshToken);

    return Good(result.Value.AccessToken);
  }

  /// <summary>
  /// Allows a client to validate the token it provides.
  /// </summary>
  /// <param name="verifyTokenDTO">The DTO containing the token to be verified.</param>
  /// <returns>A boolean indicating whether the token is valid.</returns>
  [HttpPost]
  [Route("verify")]
  public async Task<IActionResult> VerifyTokenAsync([FromBody] VerifyTokenDTO verifyTokenDTO)
  {
    ArgumentNullException.ThrowIfNull(verifyTokenDTO);
    ArgumentNullException.ThrowIfNull(verifyTokenDTO.Token);

    var result = await jwtService.ValidateTokenAsync(verifyTokenDTO.Token);

    return result.IsError ? Bad(result.Error!.Message) : Good(result.Value);
  }
}
