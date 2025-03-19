namespace CodeaculaStreamerTools.Redis.Repositories;

using CodeaculaStreamerTools.Core.Common.Results;
using CodeaculaStreamerTools.Core.Domain.Authentication.Repos;

public class StateStringRepo : IStateStringRepo
{
  public Task<OperationResult<string>> GetStateStringAsync() => Task.FromResult<OperationResult<string>>(string.Empty);

  public Task<OperationResult<bool>> ValidateStateStringAsync(string stateString) => Task.FromResult<OperationResult<bool>>(true);
}
