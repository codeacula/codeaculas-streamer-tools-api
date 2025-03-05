namespace Codeacula.Core.Tests.Services;
using Codeacula.Core.Common.CQRS;
using Codeacula.Core.Common.Errors;
using Codeacula.Core.Common.Results;
using Codeacula.Core.Services;
using Microsoft.Extensions.DependencyInjection;

public class MediatorServiceTests
{
  private readonly IServiceProvider serviceProvider;

  public MediatorServiceTests()
  {
    var services = new ServiceCollection();
    _ = services.AddTransient<ICommandHandler<TestCommand, string>, TestCommandHandler>();
    _ = services.AddTransient<IQueryHandler<TestQuery, string>, TestQueryHandler>();
    serviceProvider = services.BuildServiceProvider();
  }

  [Fact]
  public async Task QueryAsync_ValidQuery_ReturnsSuccessAsync()
  {
    // Arrange
    var mediator = new MediatorService(serviceProvider);
    var query = new TestQuery("test");

    // Act
    var result = await mediator.ExecuteQueryAsync(query);

    // Assert
    Assert.True(result.IsSuccess);
    Assert.Equal("test-queried", result.Value);
  }

  [Fact]
  public async Task ExecuteCommandAsync_NoHandlerFound_ReturnsInvalidCommandErrorAsync()
  {
    var mediator = new MediatorService(serviceProvider);
    var result = await mediator.ExecuteCommandAsync(new UnsupportedCommand());
    Assert.False(result.IsSuccess);
    _ = Assert.IsType<InvalidCommandError>(result.Error);
  }

  [Fact]
  public async Task QueryAsync_NoHandlerFound_ReturnsInvalidQueryErrorAsync()
  {
    var mediator = new MediatorService(serviceProvider);
    var result = await mediator.ExecuteQueryAsync(new UnsupportedQuery());
    Assert.False(result.IsSuccess);
    _ = Assert.IsType<InvalidQueryError>(result.Error);
  }

  private sealed record TestCommand(string Data) : ICommand<string>
  {
    public Guid Id => Guid.NewGuid();

    public long Version => 1;

    public DateTime CreatedAt => DateTime.UtcNow;
  }

  private sealed record TestQuery(string Data) : IQuery<string>;

  private sealed record UnsupportedCommand() : ICommand<string>
  {
    public Guid Id => Guid.NewGuid();

    public long Version => 1;

    public DateTime CreatedAt => DateTime.UtcNow;
  }

  private sealed record UnsupportedQuery() : IQuery<string>;

  private sealed record NullCommand() : ICommand<string>
  {
    public Guid Id => Guid.NewGuid();

    public long Version => 1;

    public DateTime CreatedAt => DateTime.UtcNow;
  }

  private sealed class NullCommandHandler : ICommandHandler<NullCommand, string>
  {
    public Task<OperationResult<string>> HandleAsync(NullCommand command) => Task.FromResult<OperationResult<string>>(string.Empty)!;
  }

  private sealed class TestCommandHandler : ICommandHandler<TestCommand, string>
  {
    public Task<OperationResult<string>> HandleAsync(TestCommand command)
        => Task.FromResult<OperationResult<string>>($"{command.Data}-processed");
  }

  private sealed class TestQueryHandler : IQueryHandler<TestQuery, string>
  {
    public Task<OperationResult<string>> HandleAsync(TestQuery query)
        => Task.FromResult<OperationResult<string>>($"{query.Data}-queried");
  }
}
