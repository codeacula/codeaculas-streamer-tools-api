namespace CodeaculaStreamerTools.Core.Services;

using System.Diagnostics.CodeAnalysis;
using CodeaculaStreamerTools.Core.Common.CQRS;
using CodeaculaStreamerTools.Core.Common.Results;
using CodeaculaStreamerTools.Core.Common.Services;

/// <summary>
/// Opted to exclude from code coverage since all this service does is invoke the appropriate handler.
/// In practice, the application should fail before reaching this point if a handler is missing.
/// </summary>
/// <param name="serviceProvider">The service provider to resolve handlers.</param>
[ExcludeFromCodeCoverage]
public class MediatorService(IServiceProvider serviceProvider) : IMediatorService
{
  private readonly IServiceProvider serviceProvider = serviceProvider;

  public async Task<OperationResult<TResult>> ExecuteCommandAsync<TResult>(ICommand<TResult> command)
  {
    try
    {
      var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));
      var handler = serviceProvider.GetService(handlerType)
          ?? throw new InvalidOperationException($"No handler found for command {command.GetType().Name}");
      var method = handlerType.GetMethod("Handle")
          ?? throw new InvalidOperationException($"Handle method not found for {handlerType.Name}");
      var task = (Task<OperationResult<TResult>>?)method.Invoke(handler, [command])
          ?? throw new InvalidOperationException("Handler returned null");
      return await task;
    }
    catch (InvalidOperationException ex)
    {
      return new InvalidCommandError(ex.Message);
    }
  }

  public async Task<OperationResult<TResult>> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
  {
    try
    {
      var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
      var handler = serviceProvider.GetService(handlerType)
          ?? throw new InvalidOperationException($"No handler found for query {query.GetType().Name}");
      var method = handlerType.GetMethod("HandleAsync")
          ?? throw new InvalidOperationException($"HandleAsync method not found for {handlerType.Name}");
      var task = (Task<OperationResult<TResult>>?)method.Invoke(handler, [query])
          ?? throw new InvalidOperationException("Handler returned null");
      return await task;
    }
    catch (InvalidOperationException ex)
    {
      return new InvalidQueryError(ex.Message);
    }
  }
}
