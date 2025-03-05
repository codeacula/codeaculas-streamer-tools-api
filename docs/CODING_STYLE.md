# Coding Style Guide

## Overview

This guide defines the coding standards and patterns used in Codeacula's Streamer Tools.

## Core Principles

- **Follow `.editorconfig` settings**: Adhere to project-specific formatting, naming conventions, and linting rules.
- **Domain-Driven Design (DDD)**:
  - Use **value objects**, **aggregates**, and **domain events** where appropriate.
  - **Encapsulate business logic inside domain models** instead of placing it in controllers or services.
  - Favor **factories** and **domain-specific constructors** over public setters.
- **CQRS (Command Query Responsibility Segregation)**:
  - **Separate commands from queries** in application logic.
  - Command handlers should **not return domain objects**—only identifiers or result types.
  - Query handlers should **never modify state**.
  - All operations must return `OperationResult<T>`.
  - Use `IMediatorService` to execute commands and queries.
- **Event Sourcing**:
  - All domain mutations must **emit events** instead of directly modifying state.
  - Events should be **immutable, append-only**, and **past-tense** (`UserRegistered`, `StreamStarted`).
- **RESTful API Consistency**:
  - Use hierarchical structures (`/users/me` instead of `/me`).
  - Maintain proper HTTP verbs (`GET`, `POST`, `PUT/PATCH`, `DELETE`).
  - Implement **resource-based routing** and **avoid action-based endpoints**.
  - Return standardized `ApiResponse<T>` from all endpoints.

## General Coding Standards

- **Follow DRY (Don't Repeat Yourself) and KISS (Keep It Simple, Stupid) principles.**
- **Use explicit over implicit** whenever possible.
- **Avoid magic numbers and strings**—use constants or configuration values.
- **Code should be self-documenting**—write meaningful variable, function, and class names.

## C# Coding Standards

### **0. Language Version & Features**

- Use C# 12.0 features where applicable
- Prefer file-scoped namespaces
- Use primary constructors for dependency injection
- Take advantage of required members
- Use pattern matching with switch expressions

### **1. Command Query Responsibility Segregation (CQRS)**

Commands, queries, and handlers must follow strict organizational and implementation patterns:

#### Directory Structure

```plaintext
Domain/{Context}/
  ├── Commands/           # Command definitions
  ├── Queries/           # Query definitions
  ├── Handlers/         # Command and query handlers
  ├── Results/          # Operation results
  ├── Events/           # Domain events
  ├── Models/           # Domain models
  └── Repos/            # Repository interfaces
```

#### Commands

- Must inherit from `CommandBase<TResult>`
- Include required metadata (Id, Version, CreatedAt)
- Be immutable records using primary constructor syntax
- Example:

```csharp
public record MyCommand(string Property) : CommandBase<MyResult>;
```

#### Queries

- Must inherit from `QueryBase`
- Must implement `IQuery<TResult>`
- Be immutable records
- Example:

```csharp
public sealed record MyQuery : QueryBase, IQuery<MyResult>;
```

#### Handlers

- Must be sealed and internal classes
- Must implement appropriate handler interface (`ICommandHandler<TCommand, TResult>` or `IQueryHandler<TQuery, TResult>`)
- Use constructor injection for dependencies via primary constructor syntax
- Return `OperationResult<T>`
- Follow consistent error handling patterns
- Example:

```csharp
internal sealed class MyHandler(
  ILogger<MyHandler> logger,
  IService service
) : IQueryHandler<MyQuery, MyResult>
{
  public async Task<OperationResult<MyResult>> HandleAsync(MyQuery query)
  {
    try
    {
      // Implementation
      return new SuccessResult<MyResult>(result);
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "Error handling query");
      return new ErrorResult<MyResult>("Specific error message");
    }
  }
}
```

### **2. Controller Standards**

- Must inherit from `BaseController<T>`
- Must use `[ApiController]` and `[Route]` attributes
- Use standard response methods via the HandleResultAsync helper:

```csharp
// Success with data
return await HandleResultAsync(() => Mediator.ExecuteQueryAsync(query));

// Direct responses
return Good(data);    // Success with data
return Bad(reason);   // Bad request with reason
return Err(reason);   // Server error with logging
```

- Use `IMediatorService` for executing commands/queries via the protected `Mediator` property
- Use XML comments for public endpoints with `<summary>`, `<param>`, `<returns>`, and `<exception>` tags where applicable
- Follow RESTful routing conventions
- All responses must be wrapped in `ApiResponse<T>`
- Use parameter validation with `ArgumentNullException.ThrowIfNull()`
- Handle all possible result types in switch expressions

### **3. Error Handling & Logging**

- All handlers must return `OperationResult<T>`:
  - Use `SuccessResult<T>` for successful operations
  - Use specific error types from `Common/Errors` namespace (e.g., `ValidationError`, `NotFoundError`)
  - Chain error results using the `IsError` and `Error!` properties
- Never throw exceptions for expected failures
- Validate all incoming parameters with `ArgumentNullException.ThrowIfNull()`
- Handle and convert all service errors appropriately
- Use result pattern with pattern matching:

```csharp
return result switch
{
  SuccessResult<T> success => HandleSuccess(success.Value),
  FailureResult<T> failure => failure.Error!,
  _ => throw new NotSupportedException()
};
```

### **4. Testing Requirements**

- **General Testing Requirements**
  - Tests must be organized to mirror the structure of the tested code
  - Every handler requires both unit and integration tests
  - Use Moq for mocking dependencies
  - Name tests using `MethodName_Scenario_ExpectedResult` pattern
  - Test both success and failure paths

- **Unit Testing**
  - Use constructor injection and setup in test class constructor
  - Follow AAA (Arrange-Act-Assert) pattern with clear section comments
  - Mock all external dependencies
  - Example pattern for Handler tests:

```csharp
public class HandlerTests
{
  private readonly Mock<IService> _mockService;
  private readonly ConfigSettings _settings;
  private readonly Handler _handler;

  public HandlerTests()
  {
    _mockService = new Mock<IService>();
    _settings = new ConfigSettings
    {
      Required = "test-value"
    };
    _handler = new Handler(_mockService.Object, _settings);
  }

  [Fact]
  public async Task HandleAsync_WithValidInput_ReturnsSuccessAsync()
  {
    // Arrange
    var command = new TestCommand("test");
    _mockService.Setup(s => s.DoSomething())
      .ReturnsAsync(new SuccessResult<string>("result"));

    // Act
    var result = await _handler.HandleAsync(command);

    // Assert
    Assert.True(result.IsSuccess);
    Assert.Equal("result", result.Value);
    _mockService.Verify(s => s.DoSomething(), Times.Once);
  }

  [Fact]
  public async Task HandleAsync_WhenServiceFails_ReturnsErrorAsync()
  {
    // Arrange
    var command = new TestCommand("test");
    var error = new ValidationError("test error");
    _mockService.Setup(s => s.DoSomething())
      .ReturnsAsync(new FailureResult<string>(error));

    // Act
    var result = await _handler.HandleAsync(command);

    // Assert
    Assert.True(result.IsError);
    Assert.Equal(error, result.Error);
  }
}
```

- **Integration Testing**
  - Use `WebApplicationFactory<Program>` for API testing
  - Inherit from `IClassFixture<WebApplicationFactory<Program>>`
  - Configure test services using `WithWebHostBuilder`
  - Test complete request/response cycles
  - Verify response status codes and content
  - Example pattern:

```csharp
public class ControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
  private readonly WebApplicationFactory<Program> _factory;
  private readonly Mock<IService> _serviceMock;

  public ControllerTests(WebApplicationFactory<Program> factory)
  {
    _factory = factory;
    _serviceMock = new Mock<IService>();
  }

  [Fact]
  public async Task Endpoint_Scenario_ReturnsExpectedResult()
  {
    // Arrange
    _serviceMock.Setup(/* ... */);
    var client = _factory.WithWebHostBuilder(builder =>
    {
      builder.ConfigureServices(services =>
      {
        services.AddSingleton(_serviceMock.Object);
      });
    }).CreateClient();

    // Act
    var response = await client.GetAsync("/endpoint");

    // Assert
    response.EnsureSuccessStatusCode();
    var result = await response.Content.ReadFromJsonAsync<ApiResponse<T>>();
    Assert.NotNull(result);
    Assert.True(result.Success);
  }
}
```

- **Test Data Requirements**
  - Use meaningful test data that represents real scenarios
  - Avoid magic strings/numbers in tests
  - Consider using test data builders or factories
  - Keep test setup DRY using constructor initialization

### **5. Formatting**

- Use **`var` when the type is apparent**, as specified in `.editorconfig`.
- Avoid `var` in cases where the type is not immediately clear for readability.
- Use **2 spaces for indentation**, no tabs.
- Use **camelCase** for local variables and parameters.
- Use **PascalCase** for class, method, and property names.
- Use **underscore prefix (`_camelCase`)** for private fields.
- Use **file-scoped namespaces** where applicable.
- Limit line length to **120 characters**.
- Place **opening braces on a new line**:

  ```csharp
  public class ExampleClass
  {
    private int _count;

    public void DoSomething()
    {
      if (_count > 0)
      {
        Console.WriteLine("Hello");
      }
    }
  }
  ```

### **6. Naming Conventions**

- Prefix **third-party provider integrations** with their name (`TwitchClient`, `YouTubeService`).
- Keep **command and query handlers suffixed** (`RegisterUserCommandHandler`, `GetUserQueryHandler`).
- Classes, interfaces, and enums should use **PascalCase**.

  ```csharp
  public class TwitchService {}
  public interface ICacheProvider {}
  public enum UserRole {}
  ```

- Methods should use **PascalCase**.

  ```csharp
  public void SendMessage() {}
  ```

- Private fields should use `_camelCase`.

  ```csharp
  private int _maxRetries;
  ```

- Constants should use **PascalCase** with `const` keyword.

  ```csharp
  private const int DefaultTimeout = 30;
  ```

### **7. Comments & Documentation**

- Use XML comments (`///`) for **public APIs** on controllers.
- Use single-line (`//`) comments for explaining complex logic.
- Avoid unnecessary comments—let the code be self-explanatory.

  ```csharp
  /// <summary>
  /// Authenticates a user via Twitch OAuth.
  /// </summary>
  public void AuthenticateUser() {}
  ```

### **8. Exception Handling & Service Implementation**

- Use **specific error types** and avoid throwing exceptions
- Services should return `OperationResult<T>` for all operations
- Use domain-specific error types (e.g., `JwtConversionError`, `TokenGenerationError`)
- Handle known failure cases with specific error responses
- Chain error results using the `IsError` and `Error!` properties
- Example service pattern:

```csharp
internal sealed class ExampleService(IConfiguration config) : IExampleService
{
  public OperationResult<string> ProcessData(string input)
  {
    try
    {
      // Implementation
      if (validationFailed)
      {
        return new ValidationError("Specific validation message");
      }

      return "processed result";
    }
    catch (SpecificException ex)
    {
      return new DomainSpecificError(ex.Message);
    }
  }
}
```

### **9. Service Configuration**

- Use strongly-typed configuration settings with primary constructor injection
- Initialize readonly fields in the constructor using config values
- Use early validation of configuration values
- Example:

```csharp
internal sealed class ServiceImpl(ServiceConfigSettings settings) : IService
{
  private readonly string _endpoint = settings.Endpoint;
  private readonly int _timeout = settings.Timeout;

  // Service implementation
}
```

### **10. Async/Await Best Practices**

- All I/O-bound operations must be async
- Use `Task` return type for async methods
- Avoid `async void` except for event handlers
- Avoid CPU-bound work on I/O threads
- Always use batch processing over repeated queries
- Properly handle task cancellation
- Example:

```csharp
public async Task<OperationResult<TResult>> HandleAsync(
  TCommand command,
  CancellationToken cancellationToken = default)
{
  try
  {
    var results = await _repository.GetBatchAsync(
      command.Ids,
      cancellationToken);

    return new SuccessResult<TResult>(results);
  }
  catch (OperationCanceledException)
  {
    throw; // Let middleware handle cancellation
  }
  catch (Exception ex)
  {
    _logger.LogError(ex, "Error processing batch");
    return new ErrorResult<TResult>("Failed to process batch");
  }
}
```

### **11. Performance & Data-Oriented Programming (DOP)**

- **Prefer structs (`struct` and `readonly struct`)** over classes for **small, immutable** objects.
- **Minimize heap allocations**; use **arrays (`T[]`), `Span<T>`, and `Memory<T>`** instead of `List<T>` when feasible.
- **Avoid unnecessary object-oriented abstractions**; use **composition over inheritance**.
- **Leverage ECS-like patterns** where applicable (e.g., for stream overlays and interactive UI logic).
- **Ensure data is stored in contiguous memory** to maximize **cache efficiency**.

### **12. Configuration & Settings**

- Use strongly-typed configuration classes
- Configuration classes must:
  - Be immutable with init-only properties
  - Use `required` keyword for mandatory settings
  - Follow naming pattern `{Feature}ConfigSettings`
- Validate configuration during startup
- Example:

```csharp
public sealed class TwitchConfigSettings
{
  public required string ClientId { get; init; }
  public required string ClientSecret { get; init; }
  public string? OptionalSetting { get; init; }
}
```

### **13. Middleware & Application Setup**

Follow strict middleware ordering:

1. Exception handling
2. HTTPS redirection
3. Static files
4. CORS
5. Authentication
6. Authorization
7. Routing

Example setup:

```csharp
public static class ApplicationBuilderExtensions
{
  public static IApplicationBuilder UseAppMiddleware(
    this IApplicationBuilder app)
  {
    return app
      .UseExceptionHandler()
      .UseHttpsRedirection()
      .UseStaticFiles()
      .UseCors()
      .UseAuthentication()
      .UseAuthorization()
      .UseRouting();
  }
}
```

### **14. API Response Standards**

All API responses must use the `ApiResponse<T>` wrapper:

```csharp
public sealed class ApiResponse<T>
{
  public bool Success { get; init; }
  public T? Data { get; init; }
  public string? ErrorCode { get; init; }
  public string? ErrorMessage { get; init; }
  public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
```

Controller methods should use standard response helpers:

```csharp
// Success response
return Good(data);

// Client error (400)
return Bad("Invalid input");

// Server error (500)
return Err("Internal error");
```

### **15. Database Access**

- Use repository pattern for data access
- Repositories must implement appropriate interfaces
- Use event sourcing for write operations
- Use read models for queries
- Handle concurrency with versioning
- Example repository:

```csharp
public sealed class UserRepository : IUserRepo
{
  private readonly IDbContext _context;

  public UserRepository(IDbContext context)
  {
    _context = context;
  }

  public async Task<OperationResult<User>> GetByIdAsync(
    Guid id,
    CancellationToken cancellationToken = default)
  {
    try
    {
      var user = await _context.Users
        .FindAsync([id], cancellationToken);

      return user is null
        ? new NotFoundError($"User {id} not found")
        : new SuccessResult<User>(user);
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error fetching user {Id}", id);
      return new ErrorResult<User>("Database error");
    }
  }
}
```

### **15. Dependency Injection & Constructor Patterns**

- Use C# 12 primary constructor syntax for dependency injection
- Declare dependencies as readonly fields when they need field access
- Keep constructors minimal, preferring initialization in declarations
- Use sealed classes by default unless inheritance is specifically needed
- Example:

```csharp
internal sealed class ExampleHandler(
  ILogger<ExampleHandler> logger,
  IExampleService service,
  ExampleSettings settings) : ICommandHandler<ExampleCommand, ExampleResult>
{
  private readonly string _baseUrl = settings.BaseUrl; // Field initialization

  public async Task<OperationResult<ExampleResult>> HandleAsync(ExampleCommand command)
  {
    // Implementation using injected dependencies
    await service.DoSomethingAsync();
    logger.LogInformation("Operation completed");
  }
}
```

## Summary

Following these guidelines ensures:

- Consistent, maintainable code
- Clear separation of concerns
- Proper error handling and logging
- Comprehensive test coverage
- Scalable architecture

All contributions must adhere to these standards and will be enforced during code review.

🚀 Happy coding!
