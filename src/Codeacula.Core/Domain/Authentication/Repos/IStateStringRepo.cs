namespace Codeacula.Core.Domain.Authentication.Repos;

using Codeacula.Core.Common.Results;

public interface IStateStringRepo
{
  Task<OperationResult<string>> GetStateStringAsync();

  Task<OperationResult<bool>> ValidateStateStringAsync(string stateString);
}
