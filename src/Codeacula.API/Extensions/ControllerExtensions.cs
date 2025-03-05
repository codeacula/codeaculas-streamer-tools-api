namespace Codeacula.API.Extensions;

using Codeacula.API.Responses;
using Codeacula.Core.Common.Results;
using Microsoft.AspNetCore.Mvc;

internal static class ControllerExtensions
{
  public static async Task<IActionResult> HandleResultAsync<T>(this ControllerBase controller, Func<Task<OperationResult<T>>> resultTaskFunc)
  {
    var result = await resultTaskFunc();
    return result switch
    {
      SuccessResult<T> success => controller.Ok(ApiResponse.Ok(success.Value)),
      FailureResult<T> failure => controller.BadRequest(ApiResponse.Error(failure.ErrorMessage)),
      _ => throw new NotSupportedException(),
    };
  }
}
