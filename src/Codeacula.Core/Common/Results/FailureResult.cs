namespace Codeacula.Core.Common.Results;

public sealed record FailureResult<T> : OperationResult<T>
{
  public FailureResult(BaseError error)
  {
    Error = error;
  }
}
