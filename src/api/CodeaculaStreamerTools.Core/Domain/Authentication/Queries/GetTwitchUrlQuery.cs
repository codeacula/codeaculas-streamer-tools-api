namespace CodeaculaStreamerTools.Core.Domain.Authentication.Queries;

using CodeaculaStreamerTools.Core.Common.CQRS;

public record GetTwitchUrlQuery() : QueryBase, IQuery<string>;
