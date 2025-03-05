namespace Codeacula.Core.Common.Services;

using Codeacula.Core.Common.CQRS;
using Codeacula.Core.Common.Results;

public interface IMediatorService
{
  Task<OperationResult<TResult>> ExecuteCommandAsync<TResult>(ICommand<TResult> command);

  Task<OperationResult<TResult>> ExecuteQueryAsync<TResult>(IQuery<TResult> query);
}
