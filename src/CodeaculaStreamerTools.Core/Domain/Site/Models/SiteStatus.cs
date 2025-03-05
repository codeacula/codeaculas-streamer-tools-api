namespace CodeaculaStreamerTools.Core.Domain.Site.Models;

public readonly record struct SiteStatus
{
  public bool IsOnline { get; init; }

  public string Version { get; init; }

  public DateTime LastUpdated { get; init; }
}
