namespace CodeaculaStreamerTools.Core.Domain.Authentication.Repos;

using CodeaculaStreamerTools.Core.Common.Results;

public interface IStateStringRepo
{
  Task<OperationResult<string>> GetStateStringAsync();

  Task<OperationResult<bool>> ValidateStateStringAsync(string stateString);
}
