namespace CodeaculaStreamerTools.Core.Common.Errors;

public record NoValueReturnedError(string Msg = "No value was returned.") : BaseError(Msg);
