namespace Codeacula.Core.Common.Errors;

public record InvalidCommandError(string Msg) : BaseError(Msg);
