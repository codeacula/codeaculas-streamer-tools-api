namespace Codeacula.Core.Domain.Site.Handlers;

using Codeacula.Core.Common.CQRS;
using Codeacula.Core.Common.Results;
using Codeacula.Core.Domain.Site.Models;
using Codeacula.Core.Domain.Site.Queries;

public sealed class SiteStatusQueryHandler : IQueryHandler<SiteStatusQuery, SiteStatus>
{
  public Task<OperationResult<SiteStatus>> HandleAsync(SiteStatusQuery query)
  {
    // For now returning a basic response. This can be enhanced later to check various system states
    var status = new SiteStatus
    {
      IsOnline = true,
      Version = "1.0.0",
      LastUpdated = DateTime.UtcNow,
    };

    return Task.FromResult<OperationResult<SiteStatus>>(new SuccessResult<SiteStatus>(status));
  }
}
