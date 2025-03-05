namespace Codeacula.Core.Common.Errors;

public record InvalidQueryError(string Msg) : BaseError(Msg);
