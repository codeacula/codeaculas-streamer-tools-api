namespace Codeacula.Core.Domain.Authentication.Exceptions;

public class UnableToDeleteFromCacheException : Exception
{
  public UnableToDeleteFromCacheException()
    : base("There was an error when attempting to delete an item from the cache.")
  {
  }

  public UnableToDeleteFromCacheException(string message)
    : base(message)
  {
  }

  public UnableToDeleteFromCacheException(string message, Exception innerException)
    : base(message, innerException)
  {
  }
}
