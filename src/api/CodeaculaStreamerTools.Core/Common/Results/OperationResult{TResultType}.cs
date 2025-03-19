namespace CodeaculaStreamerTools.Core.Common.Results;

using System.Text.Json.Serialization;

public abstract record OperationResult<TResultType>
{
  public BaseError? Error { get; init; }

  public bool IsSuccess => this is SuccessResult<TResultType>;

  public bool IsError => this is FailureResult<TResultType>;

  [JsonIgnore]
  public TResultType Value => this is SuccessResult<TResultType> { Data: var data }
      ? data
      : throw new InvalidOperationException("No value present");

  [JsonIgnore]
  public string ErrorMessage => this is FailureResult<TResultType> { Error: var error } && error is not null
      ? error
      : throw new InvalidOperationException("No error present");

  public static implicit operator OperationResult<TResultType>(TResultType value)
  {
    return new SuccessResult<TResultType>(value);
  }

  public static implicit operator TResultType(OperationResult<TResultType> result)
  {
    ArgumentNullException.ThrowIfNull(result);
    return result.Value;
  }

  public static implicit operator OperationResult<TResultType>(BaseError error)
  {
    return new FailureResult<TResultType>(error);
  }

  public OperationResult<TResultType> Err(BaseError error) => new FailureResult<TResultType>(error);

  public OperationResult<TResultType> ToOperationResult() => this;

  public TResultType ToT() => Value;
}
