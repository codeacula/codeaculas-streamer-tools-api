namespace CodeaculaStreamerTools.Core.Common.Errors;

public record InvalidCommandError(string Msg) : BaseError(Msg);
