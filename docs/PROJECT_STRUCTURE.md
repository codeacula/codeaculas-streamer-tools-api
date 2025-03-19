# Project Structure

## Overview

Codeacula's Streamer Tools follows a modular, well-structured architecture to ensure maintainability, scalability, and ease of development. Below is a breakdown of the project structure and its components.

## Directory Layout

```plaintext
/codeaculas-streamer-tools
├── src/                 # Main application source code
│   ├── CodeaculaStreamerTools.API/   # REST API service
│   ├── CodeaculaStreamerTools.Core/  # Domain logic & event sourcing
│   ├── CodeaculaStreamerTools.Infrastructure/ # External integrations
│   ├── CodeaculaStreamerTools.MSSQL/ # SQL Server implementation
│   ├── CodeaculaStreamerTools.MongoDB/ # Read models (MongoDB)
│   ├── CodeaculaStreamerTools.Redis/ # Caching and Pub/Sub
├── tests/               # Unit & Integration tests
├── config/              # Docker, environment, and deployment configs
├── scripts/             # Utility scripts for migrations, tests, etc.
├── docs/                # Documentation & technical references
├── .github/             # GitHub Actions, PR templates, and workflows
```

## Component Breakdown

### **1. `src/` - Source Code**

This is where the core application resides. It is broken down into several submodules:

- **CodeaculaStreamerTools.API/** – The primary REST API, handling requests, authentication, and routing.
- **CodeaculaStreamerTools.Core/** – Contains business logic, domain models, and event sourcing components.
- **CodeaculaStreamerTools.Infrastructure/** – Handles external integrations such as Twitch API, OBS WebSockets, and third-party services.
- **CodeaculaStreamerTools.MSSQL/** – Implements persistence logic using SQL Server.
- **CodeaculaStreamerTools.MongoDB/** – Manages read models using MongoDB for CQRS.
- **CodeaculaStreamerTools.Redis/** – Implements caching and Pub/Sub functionality for real-time communication.

### **2. `tests/` - Testing**

This directory contains:

- **Unit tests** covering core functionality.
- **Integration tests** validating API interactions and database consistency.
- **Test utilities** for mocking dependencies.

### **3. `config/` - Configuration Files**

Contains configuration files for:

- **Docker Compose** (`docker-compose.override.yml`, `docker-compose.prod.yml`).
- **Application settings** (`appsettings.json`).
- **Deployment scripts**.

### **4. `scripts/` - Utility Scripts**

Scripts for managing the development workflow, including:

- Database migrations.
- Test execution automation.
- Development startup utilities (`start-dev.ps1`).

### **5. `docs/` - Documentation**

Contains:

- **Project structure guidelines**.
- **API documentation**.
- **Setup and deployment guides**.

### **6. `.github/` - GitHub Workflows & PR Templates**

Includes:

- **GitHub Actions** workflows for CI/CD.
- **Pull request templates** enforcing contribution guidelines.
- **Issue templates** for structured bug reporting.

## Best Practices

- **Follow Domain-Driven Design (DDD)**: Core logic remains in `CodeaculaStreamerTools.Core/`.
- **Adopt CQRS/Event Sourcing**: Read models are separate from event-based writes.
- **Use containerization**: Services are designed to run in **Dockerized environments**.
- **Follow the commit guidelines**: Use structured commit messages for maintainability.

## Summary

This structure ensures a clear separation of concerns, making it easier to scale and extend Codeacula's Streamer Tools over time. Adhering to these conventions will help maintain the integrity of the codebase.

🚀 Happy coding!
