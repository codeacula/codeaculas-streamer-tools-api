namespace Codeacula.Core.Common.Errors;

public record HttpRequestFailedError(string Msg) : BaseError(Msg);
