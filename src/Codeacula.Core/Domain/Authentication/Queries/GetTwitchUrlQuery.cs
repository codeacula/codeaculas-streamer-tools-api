namespace Codeacula.Core.Domain.Authentication.Queries;

using Codeacula.Core.Common.CQRS;

public record GetTwitchUrlQuery() : QueryBase, IQuery<string>;
