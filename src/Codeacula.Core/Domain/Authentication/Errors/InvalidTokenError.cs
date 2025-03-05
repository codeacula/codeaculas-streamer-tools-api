namespace Codeacula.Core.Domain.Authentication.Errors;

public record InvalidTokenError(string Msg = "The provided token was invalid.") : BaseError(Msg);
