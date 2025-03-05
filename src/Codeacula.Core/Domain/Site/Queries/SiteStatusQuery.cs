namespace Codeacula.Core.Domain.Site.Queries;

using Codeacula.Core.Common.CQRS;
using Codeacula.Core.Domain.Site.Models;

public record SiteStatusQuery() : QueryBase, IQuery<SiteStatus>;
