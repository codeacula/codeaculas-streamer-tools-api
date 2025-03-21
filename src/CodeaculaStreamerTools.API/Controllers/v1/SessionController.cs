namespace CodeaculaStreamerTools.API.Controllers.V1;

using CodeaculaStreamerTools.Core.Common.Services;
using CodeaculaStreamerTools.Core.Domain.Site.Queries;
using Microsoft.AspNetCore.Mvc;

public sealed class SessionController(
  ILogger<SessionController> logger,
  IMediatorService mediatorService) : BaseController<SessionController>(logger, mediatorService)
{
  /// <summary>
  /// Gets the current site status.
  /// </summary>
  /// <returns>The current site status information.</returns>
  [HttpGet("status")]
  public async Task<IActionResult> GetStatusAsync() => await this.HandleResultAsync(() => Mediator.ExecuteQueryAsync(new SiteStatusQuery()));
}
