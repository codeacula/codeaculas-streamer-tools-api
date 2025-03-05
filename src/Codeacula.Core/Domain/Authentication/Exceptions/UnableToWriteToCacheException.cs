namespace Codeacula.Core.Domain.Authentication.Exceptions;

public class UnableToWriteToCacheException : Exception
{
  public UnableToWriteToCacheException()
  {
  }

  public UnableToWriteToCacheException(string msg = "Unable to write to cache")
    : base(msg)
  {
  }

  public UnableToWriteToCacheException(string message, Exception innerException)
    : base(message, innerException)
  {
  }
}
