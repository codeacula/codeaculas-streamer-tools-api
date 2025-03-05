namespace Codeacula.Core.Domain.Twitch.Errors;

public record TokenGenerationError(string Msg) : BaseError(Msg);
