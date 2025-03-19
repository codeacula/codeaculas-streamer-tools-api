namespace CodeaculaStreamerTools.Core.Logging;

using Microsoft.Extensions.Logging;

public static partial class LogDefinitions
{
  [LoggerMessage(EventId = 1, Level = LogLevel.Error, Message = "An error occurred during processing: {Reason}")]
  public static partial void ErrorProcessing(ILogger logger, string reason);

  [LoggerMessage(EventId = 2, Level = LogLevel.Information, Message = "Was given an empty key to delete.")]
  public static partial void LogEmptyKeyDeletionAttempt(ILogger logger);

  [LoggerMessage(EventId = 3, Level = LogLevel.Error, Message = "Bad request: {Reason}")]
  public static partial void LogBadRequest(ILogger logger, string reason);

  [LoggerMessage(EventId = 1000, Level = LogLevel.Information, Message = "Handling request: {Method} {Path}")]
  public static partial void HandlingRequestMessage(ILogger logger, string method, string path);
}
