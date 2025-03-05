namespace Codeacula.Core.Domain.Twitch.Errors;

public record TwitchNoUserData(string Msg = "Twitch failed to provide user data") : BaseError(Msg);
