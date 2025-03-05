namespace Codeacula.Core.Common.Errors;

public record ValidationError(string Msg) : BaseError(Msg);
