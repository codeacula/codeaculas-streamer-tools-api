namespace CodeaculaStreamerTools.Core.Common.CQRS;

using CodeaculaStreamerTools.Core.Common.Results;

public interface IQueryHandler<in TQuery, TResult>
  where TQuery : IQuery<TResult>
{
  Task<OperationResult<TResult>> HandleAsync(TQuery query);
}
