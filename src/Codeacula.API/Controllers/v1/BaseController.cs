namespace Codeacula.API.Controllers.V1;

using Codeacula.API.Responses;
using Codeacula.Core.Common.Services;
using Codeacula.Core.Logging;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class BaseController<TController>(ILogger<TController> logger, IMediatorService mediatorService) : ControllerBase
{
  protected ILogger<TController> Logger { get; set; } = logger;

  protected IMediatorService Mediator { get; set; } = mediatorService;

  protected BadRequestObjectResult Bad(string reason = "") => BadRequest(ApiResponse.Error(reason));

  protected ObjectResult Err(string reason = "")
  {
    LogDefinitions.LogBadRequest(Logger, reason);
    return Problem(detail: reason);
  }

  protected OkObjectResult Good<T>(T? data) => Ok(ApiResponse.Ok(data));
}
