namespace Codeacula.Core.Domain.Authentication.Errors;

public record StateStringMissingError(string Msg = "The requested state string couldn't be found in cache") : BaseError(Msg);
