namespace Codeacula.Infrastructure.Cache.Errors;

using Codeacula.Core.Common.Errors;

public record UnableToDeleteFromCacheError(string Key) : BaseError($"Failed to delete key {Key} from cache.");
