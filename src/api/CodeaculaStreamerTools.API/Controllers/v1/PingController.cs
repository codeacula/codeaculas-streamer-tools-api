namespace CodeaculaStreamerTools.API.Controllers.V1;

using CodeaculaStreamerTools.Core.Common.Services;
using Microsoft.AspNetCore.Mvc;

public class PingController(ILogger<PingController> iLogger, IMediatorService mediatorService) : BaseController<PingController>(iLogger, mediatorService)
{
  [HttpGet]
  public IActionResult Get() => Good("ping");
}
