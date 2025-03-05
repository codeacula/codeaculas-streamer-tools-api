namespace CodeaculaStreamerTools.Core.Common.Errors;

public record HttpRequestFailedError(string Msg) : BaseError(Msg);
