# Custom Instructions for C# API Project

This document outlines best practices, conventions, and custom instructions for maintaining and extending the Contacts API project.

## Project Structure
- **Controllers/**: Contains API controllers. Controllers should only handle HTTP logic and delegate business/data logic to services or repositories.
- **Data/**: Contains data access logic, including repositories and context classes.
- **Models/**: Contains data models and DTOs.
- **Tests/**: Contains unit and integration tests.

## Coding Conventions
- Use `async`/`await` for all I/O-bound operations.
- Use dependency injection for services and repositories.
- Validate models using data annotations or FluentValidation.
- Return appropriate HTTP status codes using `IActionResult` or `ActionResult<T>`.
- Secure endpoints using authentication and authorization attributes as needed.
- Use logging and exception handling middleware for error management.
- Do not store secrets in `appsettings.json`.

## API Design
- Follow RESTful conventions for endpoints (GET, POST, PUT, DELETE).
- Use meaningful route names and HTTP verbs.
- Document endpoints using XML comments or Swagger annotations.

## Testing
- Write unit tests for controllers, services, and repositories.
- Cover both positive and negative scenarios.
- Use mocking frameworks for dependencies.

## Extending the Project
- Add new features by creating new models, repositories, and controllers as needed.
- Update tests to cover new functionality.
- Document changes in this instruction file.

## Prompt Files Directory
- Store prompt templates, example requests, and custom instructions in the `prompts/` directory.

---
_Last updated: January 14, 2026_
