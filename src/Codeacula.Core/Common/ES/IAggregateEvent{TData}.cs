namespace Codeacula.Core.Common.ES;

public interface IAggregateEvent<TData> : IAggregateEvent
{
  TData Data { get; }
}
