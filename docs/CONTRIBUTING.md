# Contributing to Codeacula's Streamer Tools

Thank you for your interest in contributing to **Codeacula's Streamer Tools**! This guide outlines the process for contributing code, documentation, and bug reports to ensure a smooth and collaborative development experience.

## 🚀 How to Contribute

### **1️⃣ Fork & Clone the Repository**

1. **Fork the repository** on GitHub.
2. Clone your fork to your local machine:

   ```sh
   git clone https://github.com/your-username/codeaculas-streamer-tools.git
   cd codeaculas-streamer-tools
   ```

3. Add the upstream repository:

   ```sh
   git remote add upstream https://github.com/your-org/codeaculas-streamer-tools.git
   ```

### **2️⃣ Create a Feature Branch**

Create a branch for your feature or bug fix:

```sh
git checkout -b feature/your-feature-name
```

Follow the **branch naming convention**:

- `feature/your-feature-name` → New features
- `bugfix/fix-description` → Bug fixes
- `hotfix/critical-fix` → Critical fixes

### **3️⃣ Code Guidelines**

#### **Coding Standards**

Please follow the coding standards outlined in [**Coding Style & Best Practices**](./docs/CODING_STYLE.md).

#### **Commit Messages**

All commits must follow the **Conventional Commits** format:

```sh
feat(auth): Add Twitch OAuth token refresh endpoint
fix(core): Resolve race condition in event sourcing
refactor(db): Optimize SQL queries for performance
```

For details, check [**Commit Message Standards**](./docs/COMMIT_GUIDELINES.md).

#### **Testing Requirements**

- Ensure all code has **unit tests**.
- Run the test suite before pushing:

  ```sh
  dotnet test
  ```

- Follow test guidelines in [**Test Generation Instructions**](./docs/TESTING.md).

### **4️⃣ Submit a Pull Request (PR)**

1. Push your branch:

   ```sh
   git push origin feature/your-feature-name
   ```

2. Open a **Pull Request (PR)** against the `main` branch.
3. Fill out the PR template and link related issues.
4. Request reviews from maintainers.

Your PR will be reviewed based on:
✅ Code structure & maintainability
✅ Proper documentation
✅ Test coverage
✅ No breaking changes

### **5️⃣ Post-Merge Workflow**

Once merged:

- Pull the latest changes:

  ```sh
  git checkout main
  git pull upstream main
  ```

- Delete your feature branch:

  ```sh
  git branch -d feature/your-feature-name
  ```

## 🤝 Code Review Process

All PRs must pass a **code review** before merging. Please ensure:

- The code follows project guidelines.
- Tests cover all major changes.
- Documentation updates are included.

For details, check [**Code Review Guidelines**](./docs/CODE_REVIEW.md).

## 🐛 Reporting Issues

If you find a bug or want to request a feature:

1. **Search existing issues** to avoid duplicates.
2. Open a **new issue** with:
   - A clear **title**
   - **Steps to reproduce**
   - **Expected behavior**
   - **Screenshots/logs if applicable**

🚀 **Thank you for helping improve Codeacula's Streamer Tools!**
