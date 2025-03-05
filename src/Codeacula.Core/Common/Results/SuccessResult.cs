namespace Codeacula.Core.Common.Results;

public sealed record SuccessResult<T>(T Data) : OperationResult<T>;
