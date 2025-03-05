# API Guide

## Overview

Codeacula's Streamer Tools API follows **RESTful principles** and is designed for **extensibility, security, and performance**. This guide outlines the API's structure, authentication, common endpoints, and best practices for integration.

## Authentication

- All API requests require **JWT-based authentication**.
- Clients must include a **Bearer token** in the `Authorization` header.

  ```http
  GET /users/me
  Authorization: Bearer <your_jwt_token>
  ```

- Tokens are obtained via **OAuth with Twitch authentication**.
- Refresh tokens are stored as HTTP-only cookies.

## API Structure & Naming Conventions

- **Base URL:** `/`
- **Resource-based URIs** (e.g., `/auth`, `/users`, `/streams`).
- **Use appropriate HTTP methods:**
  - `GET` → Retrieve data
  - `POST` → Create new resources or perform actions
  - `PUT/PATCH` → Update existing resources
  - `DELETE` → Remove resources

## Common Endpoints

### **Health Check**

```http
GET /
```

- Returns "ping" to verify the API is running

### **Session Management**

```http
GET /session/status
```

- Returns the current site status

### **Authentication**

```http
GET /auth/twitch
```

- Returns a URL for Twitch OAuth authentication.

```http
POST /auth/twitch
```

- Completes the Twitch OAuth flow
- Request body:

```json
{
    "code": "string",
    "state": "string"
}
```

- Returns an access token and sets refresh token as cookie

```http
GET /auth/verify
```

- Verifies a JWT token
- Query parameters:
  - `token`: The JWT token to verify
- Returns a boolean indicating if the token is valid

## Response Format

### Standard Response Pattern

All API endpoints consistently return responses wrapped in an `ApiResponse<T>` format. This includes:

- **Successful Responses** (HTTP 200)
  - All data is wrapped in the standard response format
  - Success is set to `true`
  - Data contains the actual response payload

- **Client Errors** (HTTP 400)
  - Returned for invalid requests or validation failures
  - Success is set to `false`
  - Contains an error message describing the issue

- **Server Errors** (HTTP 500)
  - Returned for unexpected server-side failures
  - Success is set to `false`
  - Contains an error message and is logged server-side

### HTTP Status Codes and Response Format

All responses include both an HTTP status code and an `ApiResponse<T>` body:

- HTTP 200 OK
  - Indicates successful operation
  - Response body has `success: true`
  - Data contains the operation result

- HTTP 400 Bad Request
  - Indicates client-side validation failures
  - Response body has `success: false`
  - Contains specific error message for the client

- HTTP 500 Internal Server Error
  - Indicates unexpected server-side errors
  - Response body has `success: false`
  - Error is logged server-side
  - Generic error message returned to client

Even when an error status code is returned, the response body will still be a valid `ApiResponse<T>` structure.

### Operation Results

Internal operations that return `OperationResult<T>` are automatically converted to the standard API response format:

- `SuccessResult<T>` → `ApiResponse<T>` with `success: true`
- `FailureResult<T>` → `ApiResponse<T>` with `success: false` and appropriate error details

This ensures consistent error handling and response formatting across all endpoints.

### Error Logging

Server-side errors (HTTP 500) are automatically logged using structured logging:

- Error messages are logged with request context
- All 500 responses use the `Problem` response type internally
- Sensitive error details are only logged, never sent to clients
- Production environments return standardized error messages

### Error Handling

- All API responses follow a consistent format using `ApiResponse<T>`:

  ```json
  {
    "success": boolean,    // Indicates if the request was successful
    "data": T | null,     // Response data for successful requests
    "errorCode": string | null,   // Error code when success is false
    "errorMessage": string | null, // Error description when success is false
    "timestamp": string   // UTC timestamp of the response
  }
  ```

- **Success Response Example:**

  ```json
  {
    "success": true,
    "data": "https://twitch.tv/auth",
    "errorCode": null,
    "errorMessage": null,
    "timestamp": "2024-01-20T12:34:56Z"
  }
  ```

- **Error Response Example:**

  ```json
  {
    "success": false,
    "data": null,
    "errorCode": "500",
    "errorMessage": "An unexpected error occurred",
    "timestamp": "2024-01-20T12:34:56Z"
  }
  ```

- Common HTTP status codes:
  - `400 Bad Request` → Invalid parameters or request
  - `401 Unauthorized` → Missing or invalid token
  - `403 Forbidden` → Insufficient permissions
  - `404 Not Found` → Resource not found
  - `500 Internal Server Error` → Unexpected failure

## Best Practices for Consumers

- Store access tokens securely
- Use the `/auth/verify` endpoint to validate tokens before making requests
- Handle refresh token flows automatically using the HTTP-only cookie
- Follow proper error handling patterns using the consistent response format

## Summary

The Codeacula API implements secure authentication via Twitch OAuth and provides a consistent, RESTful interface for integration. By following these guidelines and best practices, developers can build reliable integrations with the platform.

🚀 Happy coding!
