namespace Codeacula.Core.Domain.Authentication.Errors;

public record UnableToSaveStateStringError(string Msg = "Unable to save generated state string.") : BaseError(Msg);
