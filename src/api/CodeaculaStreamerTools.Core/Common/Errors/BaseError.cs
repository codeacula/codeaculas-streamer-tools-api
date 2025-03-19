namespace CodeaculaStreamerTools.Core.Common.Errors;

public abstract record BaseError(string Message)
{
  public static implicit operator string(BaseError error)
  {
    ArgumentNullException.ThrowIfNull(error);
    return error.Message;
  }
}
