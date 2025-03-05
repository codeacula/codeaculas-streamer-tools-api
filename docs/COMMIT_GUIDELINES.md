# Commit Message Guidelines

## Overview

Consistent commit messages improve readability, make it easier to track changes, and enhance collaboration. This document outlines the required format for commit messages in Codeacula's Streamer Tools project.

## Format

Each commit message **must** follow the structure below:

```plaintext
<type>(<optional scope>)<optional !>: <description>

<optional body>

<optional footers>
```

### Example

```plaintext
feat(auth): Add Twitch OAuth token refresh

Implemented automatic token refresh for Twitch authentication to prevent token expiration issues.

BREAKING CHANGE: Users must reauthenticate after this update.
```

## Commit Components

### 1. **Type (Required)**

The commit type indicates the purpose of the change. Allowed types:

- **feat** – A new feature.
- **fix** – A bug fix.
- **docs** – Documentation changes.
- **style** – Code style updates (formatting, missing semicolons, etc.), not affecting functionality.
- **refactor** – Code restructuring without changing behavior.
- **perf** – Performance improvements.
- **test** – Adding or updating tests.
- **chore** – Routine tasks (dependency updates, build changes, etc.).

### 2. **Scope (Optional)**

The scope clarifies the specific area of the code affected. Example scopes:

- `auth` – Authentication module
- `db` – Database changes
- `api` – API-related updates
- `ui` – Frontend UI changes
- `orb` – Orb system updates
- `user` – User-related functionality

Format: `(scope)`, e.g., `fix(auth): Handle invalid token errors`

### 3. **Breaking Changes (Required if applicable)**

A breaking change must be indicated by **either**:

- **Adding `!` before the colon**, e.g., `feat(auth)!: Require reauthentication`
- **Using `BREAKING CHANGE:` in the footer**, e.g.:

  ```plaintext
  BREAKING CHANGE: API responses now include a `timestamp` field.
  ```

### 4. **Description (Required)**

- A **short, imperative sentence** describing the change.
- **Start with a verb** (e.g., `Add`, `Fix`, `Update`).
- **Do not capitalize the first letter** unless it's a proper noun.
- **No period at the end.**

✅ Good: `fix(api): Resolve null reference exception in request handler`
❌ Bad: `Fixed API issue.`

### 5. **Body (Optional)**

- Provide additional context if necessary.
- Separate from the description by a blank line.
- Use multiple paragraphs if needed.

### 6. **Footers (Optional, but Recommended for Metadata)**

Common footers:

- **`BREAKING CHANGE:`** – Describes breaking changes.
- **`Closes #<issue-number>`** – Links to GitHub issues.
- **`Co-authored-by:`** – Credits multiple contributors.

### 7. **Grouping Changes by Type in a Commit**

If a commit includes multiple types of changes (e.g., updates to the **orb** system and **user** functionality), each type should be grouped separately within the commit message.

Each type should be written as a separate entry within the commit description, ensuring clear categorization of changes.

#### Example of Proper Grouping in a Commit

✅ **Good PR Structure:**

```plaintext
feat(user): Add profile picture upload
fix(user): Resolve validation issue for usernames
feat(orb): Implement new Orb reward system
fix(orb): Fix incorrect reward calculations
```

❌ **Bad PR Structure (Combining Unrelated Changes):**

```plaintext
feat: Add profile picture upload and fix Orb reward calculations
```

Each type of change should be grouped logically within the commit message so that different functional areas remain distinct and easier to track.

### Commit Message Do's & Don'ts

✅ **Do:**

- Use **present tense** (`Add feature`, not `Added feature`).
- Keep messages **concise** but informative.
- Reference **related issues** (e.g., `Closes #123`).

❌ **Don't:**

- Write **vague** messages (`fix bug` is not useful).
- Include **unrelated changes** in one commit.
- Use **long titles** (keep it under ~72 characters).

## Summary

By following this structure, commit messages remain **clean, searchable, and helpful**. If a commit doesn't match these standards, **it will be rejected in code review**.

🚀 Happy coding!
