namespace CodeaculaStreamerTools.Core.Common.Errors;

public record InvalidQueryError(string Msg) : BaseError(Msg);
