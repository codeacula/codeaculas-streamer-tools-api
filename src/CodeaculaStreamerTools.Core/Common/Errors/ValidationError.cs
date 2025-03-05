namespace CodeaculaStreamerTools.Core.Common.Errors;

public record ValidationError(string Msg) : BaseError(Msg);
