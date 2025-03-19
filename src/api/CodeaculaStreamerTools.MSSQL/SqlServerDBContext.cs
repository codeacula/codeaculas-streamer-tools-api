namespace CodeaculaStreamerTools.MSSQL;

using CodeaculaStreamerTools.MSSQL.Entities;
using Microsoft.EntityFrameworkCore;

public class SqlServerDBContext(DbContextOptions<SqlServerDBContext> options) : DbContext(options), ISqlServerDBContext
{
  public required DbSet<AccessCredentialDBO> AccessCredentials { get; set; }

  public required DbSet<RefreshTokenDBO> RefreshTokens { get; set; }

  public required DbSet<RoleDBO> Roles { get; set; }

  public required DbSet<EventSourceEventDBO> UserEvents { get; set; }

  public Task<int> SaveChangesAsync() => base.SaveChangesAsync();

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseLazyLoadingProxies();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    _ = modelBuilder.Entity<EventSourceEventDBO>()
      .HasIndex(e => new { e.AggregateId, e.Version })
      .IsUnique();

    _ = modelBuilder.Entity<AccessCredentialDBO>()
      .Property(e => e.TokenType)
      .HasConversion<string>();
  }
}
