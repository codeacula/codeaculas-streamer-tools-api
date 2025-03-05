namespace CodeaculaStreamerTools.API.Infrastructure.JWT;

using CodeaculaStreamerTools.Core.Common.Errors;

internal sealed record JwtConversionError(string Msg = "Failed to convert JWT.") : BaseError(Msg);
