namespace Codeacula.Core.Common.Results;

using System.Text.Json.Serialization;

public abstract record OperationResult<T>
{
  public BaseError? Error { get; init; }

  public bool IsSuccess => this is SuccessResult<T>;

  public bool IsError => this is FailureResult<T>;

  [JsonIgnore]
  public T Value => this is SuccessResult<T> { Data: var data }
      ? data
      : throw new InvalidOperationException("No value present");

  [JsonIgnore]
  public string ErrorMessage => this is FailureResult<T> { Error: var error } && error is not null
      ? error
      : throw new InvalidOperationException("No error present");

  public static implicit operator OperationResult<T>(T value)
  {
    return new SuccessResult<T>(value);
  }

  public static implicit operator T(OperationResult<T> result)
  {
    ArgumentNullException.ThrowIfNull(result);
    return result.Value;
  }

  public static implicit operator OperationResult<T>(BaseError error)
  {
    return new FailureResult<T>(error);
  }

  public OperationResult<T> Err(BaseError error) => new FailureResult<T>(error);

  public OperationResult<T> ToOperationResult() => this;

  public T ToT() => Value;
}
