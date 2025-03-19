namespace CodeaculaStreamerTools.Core.Common.Services;

using CodeaculaStreamerTools.Core.Common.CQRS;
using CodeaculaStreamerTools.Core.Common.Results;

public interface IMediatorService
{
  Task<OperationResult<TResult>> ExecuteCommandAsync<TResult>(ICommand<TResult> command);

  Task<OperationResult<TResult>> ExecuteQueryAsync<TResult>(IQuery<TResult> query);
}
