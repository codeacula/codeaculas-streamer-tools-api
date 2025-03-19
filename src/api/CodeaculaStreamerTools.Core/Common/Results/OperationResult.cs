namespace CodeaculaStreamerTools.Core.Common.Results;

public static class OperationResult
{
  public static OperationResult<TResultType> Err<TResultType>(BaseError error) => new FailureResult<TResultType>(error);

  public static OperationResult<TResultType> Ok<TResultType>(TResultType value) => new SuccessResult<TResultType>(value);
}
