namespace CodeaculaStreamerTools.Core.Common.Errors;

public record EmptyAggregateIdError(string Msg) : BaseError(Msg);
