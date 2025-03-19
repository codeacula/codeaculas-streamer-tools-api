namespace CodeaculaStreamerTools.MSSQL;

using CodeaculaStreamerTools.MSSQL.Entities;
using Microsoft.EntityFrameworkCore;

public interface ISqlServerDBContext
{
  DbSet<AccessCredentialDBO> AccessCredentials { get; }

  DbSet<RefreshTokenDBO> RefreshTokens { get; set; }

  DbSet<RoleDBO> Roles { get; }

  DbSet<EventSourceEventDBO> UserEvents { get; }

  Task<int> SaveChangesAsync();
}
