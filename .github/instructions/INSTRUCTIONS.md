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
- Always validate phone numbers using regex for format xxx-xxx-xxxx.

## API Design

## Testing
 Write unit tests for controllers, services, and repositories.
 Cover both positive and negative scenarios.
 Always include unit tests for the following edge cases:
	 1. Not Found: Valid input but no matching record (should return NotFound).
	 2. Null or Empty: Null or empty input values (should return BadRequest).
	 3. Duplicate: Attempt to create or update with a value that already exists (should handle conflict or error).
	 4. Special Characters: Input containing special or unicode characters (should validate or reject as appropriate).
 Use mocking frameworks for dependencies.

## Extending the Project

## Prompt Files Directory
- Store prompt templates, example requests, and custom instructions in the `prompts/` directory.

---
_Last updated: January 14, 2026_
