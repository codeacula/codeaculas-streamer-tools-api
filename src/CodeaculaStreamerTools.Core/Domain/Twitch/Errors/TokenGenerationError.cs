namespace CodeaculaStreamerTools.Core.Domain.Twitch.Errors;

public record TokenGenerationError(string Msg) : BaseError(Msg);
