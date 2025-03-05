# Codeacula's Streamer Tools

## 🚀 Overview

Codeacula's Streamer Tools is a powerful, extensible system designed to enhance **Twitch stream interaction** and provide advanced automation tools. Built with **CQRS, Event Sourcing, and Domain-Driven Design (DDD)**, it offers:

- **Twitch OAuth Authentication**
- **Custom Chat Commands & Event Tracking**
- **Twitch Points & Reward Handling**
- **User & Moderator Management**
- **OBS WebSocket Integration**
- **Ad Management & Scheduling**
- **Flexible API for Future Expansions**

## 📦 Quick Start

### **Prerequisites**

Make sure you have the following installed:

- .NET 9.0+
- Docker & Docker Compose
- A Twitch Developer Account with OAuth credentials

### **Installation & Setup**

```sh
# Clone the repository
git clone https://github.com/your-org/codeacula-api.git
cd codeacula-api

# Start development environment with hot reload
./scripts/start-dev.ps1
```

### **Alternative Manual Start**

```sh
# Start services using Docker
docker-compose -f config/docker-compose.override.yml up --build -d

# Run the API manually
dotnet run --project src/Codeacula.API
```

### **Configuration**

All environment variables are managed via **Docker configuration**. Update the appropriate `docker-compose.override.yml` or `docker-compose.prod.yml` file as needed.

## 📂 Project Structure

```plaintext
/codeacula-api
├── src/                            # Main application source code
│   ├── Codeacula.API/              # REST API service
│   ├── Codeacula.Core/             # Domain logic & event sourcing
│   ├── Codeacula.Infrastructure/   # External integrations
│   ├── Codeacula.MSSQL/            # SQL Server implementation
│   ├── Codeacula.MongoDB/          # Read models (MongoDB)
│   ├── Codeacula.Redis/            # Caching and Pub/Sub
├── tests/                          # Unit & Integration tests
├── config/                         # Docker, environment, and deployment configs
├── scripts/                        # Utility scripts for migrations, tests, etc.
├── docs/                           # Documentation & technical references
├── .github/                        # GitHub Actions, PR templates, and workflows
```

## 📖 Documentation

For more details, check out the full documentation:

- [**Project Structure & Code Conventions**](./docs/PROJECT_STRUCTURE.md)
- [**Coding Style & Best Practices**](./docs/CODING_STYLE.md)
- [**Code Review Guidelines**](./docs/CODE_REVIEW.md)
- [**Commit Message Standards**](./docs/COMMIT_GUIDELINES.md)
- [**Test Generation Instructions**](./docs/TESTING.md)
- [**API Documentation**](./docs/API_GUIDE.md)

## 🤝 Contributing

### **Development Workflow**

1. **Fork & Clone**: Work on a feature branch.
2. **Follow Coding Standards**: Run linting & tests before committing.
3. **Commit Messages**: Use Conventional Commits.
4. **Submit PRs**: Ensure PRs follow review guidelines.

For detailed contribution guidelines, see [**CONTRIBUTING.md**](./docs/CONTRIBUTING.md).

## 🔥 API Overview

For full API specifications, check [**API_GUIDE.md**](./docs/API_GUIDE.md).

```sh
# Example Request
curl -X GET https://api.yourdomain.com/auth/user \
     -H "Authorization: Bearer YOUR_JWT"
```

## 📜 License

This project is private and not publicly licensed. Usage, distribution, and modifications are subject to Codeacula's terms and conditions.

---

🚀 **Built with ❤️ by the Codeacula Community**
