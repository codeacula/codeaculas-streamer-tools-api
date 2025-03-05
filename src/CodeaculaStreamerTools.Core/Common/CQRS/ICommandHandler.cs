namespace CodeaculaStreamerTools.Core.Common.CQRS;

using CodeaculaStreamerTools.Core.Common.Results;

public interface ICommandHandler<in TCommand, TResult>
  where TCommand : ICommand<TResult>
{
  /// <summary>
  /// Runs the provided command.
  /// </summary>
  /// <param name="command">The command to run.</param>
  /// <returns>If the command was successful, along with any new aggregate IDs where appropriate.</returns>
  Task<OperationResult<TResult>> HandleAsync(TCommand command);

  /// <summary>
  /// Validates the provided command.
  /// </summary>
  /// <param name="command">The command to validate.</param>
  /// <returns>Result indicating whether the validation was successful.</returns>
  Task<OperationResult<bool>> ValidateAsync(TCommand command);
}
