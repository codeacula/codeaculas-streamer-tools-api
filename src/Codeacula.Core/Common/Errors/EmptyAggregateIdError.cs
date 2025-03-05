namespace Codeacula.Core.Common.Errors;

public record EmptyAggregateIdError(string Msg) : BaseError(Msg);
