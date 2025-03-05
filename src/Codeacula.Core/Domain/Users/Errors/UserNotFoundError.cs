namespace Codeacula.Core.Domain.Users.Errors;

public record UserNotFoundError(string Msg = "The requested user could not be found") : BaseError(Msg);
