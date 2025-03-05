namespace CodeaculaStreamerTools.Infrastructure.Cache.Errors;

using CodeaculaStreamerTools.Core.Common.Errors;

public record UnableToDeleteFromCacheError(string Key) : BaseError($"Failed to delete key {Key} from cache.");
