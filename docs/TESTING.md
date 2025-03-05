# Testing Guidelines

## Test Generation Instructions

### File Structure

- Save tests in the appropriate project directory in the `Codeacula.Tests` project, maintaining a parallel structure to the code being tested.
- Append "Tests" to both the project name and file name. For instance:
  - If the code file is located at `/{ProjectName}/{FilePath}/{FileName}.cs`, the corresponding test file should be located at:
    `/Codeacula.Tests/{ProjectName}/{FilePath}/{FileName}Tests.cs`.

### Test Framework

- Tests should use the xUnit testing framework.

### Test Class Naming

- Use the same name as the class being tested, appending "Tests."
- Example: If testing `JwtValidator.cs`, the test class should be named `JwtValidatorTests`.

### Test Method Naming

- Use descriptive names for test methods that clearly state the expected behavior.
- Follow the convention: `<MethodName>_<Condition>_<ExpectedResult>`.
  - Example: `ValidateToken_ValidToken_ReturnsTrue`.
- Private methods should **not** be tested directly. Instead, they should be tested through the public API of the class that contains them to ensure proper encapsulation.

### Test Coverage

- Aim to cover:
  - All public methods.
  - Edge cases, boundary values, and failure scenarios.
  - Critical business logic.

### Mocking Dependencies

- Use appropriate mocking frameworks (e.g., Moq) to mock dependencies where needed.
- Avoid using real implementations of external dependencies (e.g., external APIs, Twitch services).
- All integration tests should use **containerized database instances** (e.g., running MSSQL/MongoDB in Docker via `docker-compose`) to ensure compatibility with the production environment. In-memory databases should not be used due to potential differences in supported features.

### Assertions

- Ensure each test includes at least one assertion.
- Use expressive assertion methods to improve readability (e.g., `Assert.Equal`, `Assert.Throws`, etc.).

### Best Practices

- Tests should be isolated and independent.
- Follow the **AAA (Arrange-Act-Assert)** pattern for organizing test code:
  1. **Arrange**: Set up the necessary test data and environment.
  2. **Act**: Execute the functionality being tested.
  3. **Assert**: Verify the expected outcome.
- Choose between static test data and randomized values based on what is most appropriate for the test case.
- Tests should be isolated and independent.
- Follow the **AAA (Arrange-Act-Assert)** pattern for organizing test code:
  1. **Arrange**: Set up the necessary test data and environment.
  2. **Act**: Execute the functionality being tested.
  3. **Assert**: Verify the expected outcome.

### Running Tests Locally

Ensure Docker containers are running before testing:

```sh
./scripts/start-dev.ps1
```

Run all tests:

```sh
dotnet test
```

### Code Coverage Standards

- PRs fail immediately if unit or integration/system test coverage drops below 100%.
- If coverage drops due to an intentional exception, the necessary attributes or configuration files must be updated accordingly.

All PRs must maintain **100% unit test coverage**, as only necessary code is included in the reporting.

For integration and system tests, **100% test coverage** is required across:

- Core business logic (CQRS handlers, authentication, Twitch API services).
- Domain models and event sourcing.
- Public API endpoints (Controllers must be tested with mock dependencies).

#### Exceptions

- Logging and telemetry functions do not require coverage because they generally do not contain business logic and are primarily used for observability rather than functionality verification.
- Configuration classes and dependency injection setups are excluded since their behavior is often determined by external factors and framework integrations.

### GitHub Actions & CI/CD

Testing is enforced via GitHub Actions:

- PRs fail if unit or integration/system test coverage drops below 100%.
- Tests are automatically executed on push and PRs.
- Coverage reports are generated for reviewers.
- Coverage enforcement is handled using **Coverlet** for collecting test coverage and **ReportGenerator** for generating human-readable reports. These tools are integrated into GitHub Actions workflows to ensure compliance with coverage thresholds.
- If a test case requires an exception to the coverage rule, the necessary attributes or files should be updated accordingly to reflect the intended exclusion.
Testing is enforced via GitHub Actions:
- PRs fail if unit or integration/system test coverage drops below 100%.
- Tests are automatically executed on push and PRs.
- Coverage reports are generated for reviewers.
- Coverage enforcement is handled using **Coverlet** for collecting test coverage and **ReportGenerator** for generating human-readable reports. These tools are integrated into GitHub Actions workflows to ensure compliance with coverage thresholds.

### Common Issues & Fixes

**Tests failing due to missing database?**

```sh
docker-compose up -d  # Ensure DB services are running
```

**Mocking Twitch API calls?**

- Use `ITwitchService` with dependency injection.
- Use `Moq` framework to mock responses.

Following these guidelines ensures reliable, maintainable, and bug-free releases.
