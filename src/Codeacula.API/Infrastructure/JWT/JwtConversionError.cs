namespace Codeacula.API.Infrastructure.JWT;

using Codeacula.Core.Common.Errors;

internal sealed record JwtConversionError(string Msg = "Failed to convert JWT.") : BaseError(Msg);
