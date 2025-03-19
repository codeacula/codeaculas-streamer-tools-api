namespace CodeaculaStreamerTools.Core.Domain.Site.Queries;

using CodeaculaStreamerTools.Core.Common.CQRS;
using CodeaculaStreamerTools.Core.Domain.Site.Models;

public record SiteStatusQuery() : QueryBase, IQuery<SiteStatus>;
