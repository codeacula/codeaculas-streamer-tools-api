# Code Review Guidelines

## Overview

Code reviews help maintain code quality, ensure adherence to project standards, and improve collaboration. This guide outlines best practices for conducting and participating in code reviews for Codeacula's Streamer Tools.

## Objectives of Code Review

- Ensure **code quality** and **readability**.
- Verify **correctness** of business logic and implementation.
- Enforce **coding style** as per [Coding Style Guide](./CODING_STYLE.md).
- Identify **performance improvements** and **potential bugs**.
- Maintain **security best practices** and **prevent vulnerabilities**.
- Ensure **CQRS, Event Sourcing, and DDD principles** are properly followed.
- Validate **API consistency** with RESTful best practices.

## Reviewer Responsibilities

1. **Check Code Structure & Maintainability:**
   - Code should follow **Domain-Driven Design (DDD)** with clear boundaries
   - Domain logic must be in appropriate handlers under `Domain/{Context}/Handlers`
   - **Required Interface Implementation**:
     - Commands must implement `ICommand<TResult>`
     - Queries must implement `IQuery<TResult>`
     - Command handlers must implement `ICommandHandler<TCommand, TResult>`
     - Query handlers must implement `IQueryHandler<TQuery, TResult>`
   - Commands must include required metadata (`Id`, `Version`, `CreatedAt`)
   - Commands must emit appropriate domain events via event sourcing pattern
   - All dependencies must be injected via constructor parameters using readonly fields
   - Services must be registered in appropriate `ServiceCollectionExtensions.cs`

2. **Verify Adherence to Standards:**
   - Follow naming conventions from [Coding Style Guide](./CODING_STYLE.md):
     - Handlers must be suffixed (e.g., `GetTwitchUrlHandler`, `LoginViaTwitchHandler`)
     - Commands must be suffixed with `Command` (e.g., `LoginViaTwitchCommand`)
     - Queries must be suffixed with `Query` (e.g., `GetTwitchUrlQuery`)
     - Results must be suffixed with `Result` (e.g., `LoginViaTwitchResult`)
   - Ensure proper file organization:
     - Commands in `Domain/{Context}/Commands`
     - Queries in `Domain/{Context}/Queries`
     - Handlers in `Domain/{Context}/Handlers`
     - Results in `Domain/{Context}/Results`
     - Events in `Domain/{Context}/Events`
   - Validate RESTful endpoints follow hierarchical structure
   - Controllers must use `IMediatorService` for executing commands/queries

3. **Configuration & Settings Validation:**
   - Configuration must use strongly-typed settings classes (e.g., `JwtConfigSettings`, `OAuthConfigSettings`)
   - Settings validation must occur during startup
   - Configuration classes must follow standard naming `{Feature}ConfigSettings`
   - Use `ConfigurationErrorsException` for missing required settings
   - Example configuration pattern:

     ```csharp
     public class FeatureConfigSettings
     {
       public required string RequiredSetting { get; init; }
       public string? OptionalSetting { get; init; }
     }
     ```

4. **Logging Standards:**
   - Use source-generated logging methods via `LoggerMessage` attributes
   - Define log messages in `LogDefinitions` static class
   - Follow standard event ID ranges:
     - 1-99: Error events
     - 100-999: Warning events
     - 1000+: Information events
   - Include relevant context in structured logging
   - Example logging pattern:

     ```csharp
     [LoggerMessage(EventId = 1, Level = LogLevel.Error,
       Message = "An error occurred during {Operation}: {Reason}")]
     public static partial void LogError(
       this ILogger logger,
       string operation,
       string reason);
     ```

5. **Error Handling & Results:**
   - Use `OperationResult<T>` for all handler responses
   - Define specific error types in `Common/Errors`
   - Error types should be immutable records
   - Include proper error context in messages
   - Example error pattern:

     ```csharp
     public sealed record ValidationError(string Message) : Error(Message);

     // Usage in handlers:
     if (!isValid)
     {
       return new ValidationError("Invalid input");
     }
     ```

6. **Assess Test Coverage & Quality:**
   - Every handler must have a corresponding test class
   - Tests must follow AAA (Arrange-Act-Assert) pattern
   - Use Moq for mocking dependencies
   - Test both success and failure paths
   - Integration tests required for API endpoints
   - Test classes must use constructor for dependency setup
   - Example test structure:

     ```csharp
     public class HandlerTests
     {
       private readonly Mock<IDependency> _mockDependency;
       private readonly Handler _handler;

       public HandlerTests()
       {
         _mockDependency = new Mock<IDependency>();
         _handler = new Handler(_mockDependency.Object);
       }

       [Fact]
       public async Task HandleAsync_WhenScenario_ThenExpectedResult()
       {
         // Arrange
         _mockDependency.Setup(...);

         // Act
         var result = await _handler.HandleAsync(command);

         // Assert
         Assert.True(result.IsSuccess);
         _mockDependency.Verify(...);
       }

       [Fact]
       public async Task HandleAsync_WhenError_ThenReturnsFailure()
       {
         // Test failure scenarios
       }
     }
     ```

7. **Check for Performance & Optimization:**
   - Verify proper use of async/await patterns
   - All I/O operations must be async
   - Check for N+1 query issues in repository calls
   - Ensure proper use of `OperationResult<T>` for all handler results
   - Validate proper error handling and propagation
   - Use appropriate caching strategies with Redis where applicable

8. **Enforce Security & Error Handling:**
   - Every handler must return `OperationResult<T>`
   - Use specific error types from `Common/Errors`
   - Authentication checks must be in appropriate middleware
   - Validate all command/query parameters
   - Configuration must be injected via strongly-typed settings classes
   - Sensitive data must use appropriate configuration patterns

9. **Verify API Response Patterns:**
   - All controllers must inherit from `BaseController<T>`
   - Use standard response methods:

     ```csharp
     return Good<T>(data);    // Success with data
     return Bad(reason);      // Bad request with reason
     return Err(reason);      // Server error with logging
     ```

   - All responses must use `ApiResponse<T>` wrapper
   - Error responses must include appropriate status codes and reasons
   - Success responses should use appropriate HTTP status codes

10. **Integration Testing Requirements:**
    - Use `WebApplicationFactory<Program>` for integration tests
    - Mock external services via test host configuration
    - Test both success and error paths
    - Verify response wrapper format
    - Example integration test pattern:

      ```csharp
      public class ControllerTests : IClassFixture<WebApplicationFactory<Program>>
      {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly Mock<IExternalService> _serviceMock;

        public ControllerTests(WebApplicationFactory<Program> factory)
        {
          _factory = factory;
          _serviceMock = new Mock<IExternalService>();
        }

        [Fact]
        public async Task Endpoint_Scenario_ExpectedResult()
        {
          // Arrange
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
          var result = await response.Content
            .ReadFromJsonAsync<ApiResponse<T>>();
          Assert.NotNull(result);
          Assert.True(result.Success);
        }
      }
      ```

11. **Middleware Configuration:**
    - Verify correct middleware order:
      1. Exception handling
      2. HTTPS redirection
      3. Static files
      4. CORS
      5. Routing
      6. Authentication
      7. Authorization
    - Environment-specific middleware configuration
    - Proper CORS policy configuration
    - JWT authentication setup
    - Cookie policy configuration

## Developer Responsibilities

1. **Follow Command/Query Pattern:**

   ```csharp
   public sealed record MyCommand : ICommand<MyResult>
   {
     public Guid Id { get; } = Guid.NewGuid();
     public long Version { get; } = 1;
     public DateTime CreatedAt { get; } = DateTime.UtcNow;

     public required string SomeProperty { get; init; }
   }

   public sealed class MyCommandHandler : ICommandHandler<MyCommand, MyResult>
   {
     private readonly IDependency _dependency;

     public MyCommandHandler(IDependency dependency)
     {
       _dependency = dependency;
     }

     public async Task<OperationResult<MyResult>> HandleAsync(MyCommand command)
     {
       // Implementation
     }
   }
   ```

2. **Write Comprehensive Tests:**
   - Every handler requires both unit and integration tests
   - Mock all external dependencies
   - Verify all error conditions
   - Test file naming must match handler being tested
   - Integration tests must use test containers where applicable
   - Use constructor injection for test setup

3. **Follow Error Handling Pattern:**
   - Use `OperationResult<T>` for all handler responses
   - Return specific error types from `Common/Errors`
   - Never throw exceptions for expected failure cases
   - Log unexpected exceptions appropriately
   - Properly propagate errors up the call chain

## Code Review Process

1. **Submit PR** with:
   - Clear title following [commit guidelines](./COMMIT_GUIDELINES.md)
   - Description of changes and testing performed
   - Links to related issues
   - Updated documentation if applicable
2. **Ensure all tests pass** before requesting review
3. **Reviewers provide feedback** within 24-48 hours
4. **Address reviewer comments** with follow-up commits
5. **All tests must pass** and coverage requirements met
6. **Merge PR** only after approval from at least one senior developer

## Summary

Code reviews ensure the reliability and maintainability of Codeacula's Streamer Tools. By following these specific guidelines and patterns already established in the codebase, we maintain consistency and quality across the project.

🚀 Happy reviewing!
