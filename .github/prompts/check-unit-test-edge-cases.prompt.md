# Prompt: Check and Add Edge Case Unit Tests

## Task
- Review all unit tests for the current project.
- Identify any missing edge case coverage for API endpoints, especially for input validation (e.g., email, phone, null/empty, whitespace, long strings, case sensitivity).
- Add new unit tests for any missing edge cases, following the project's existing test formatting and conventions (use Moq for mocking, assert formatting, etc).
- Update or refactor test data to match the formatting and structure of previous tests.
- Ensure all tests include both success and failure scenarios (e.g., Ok, NotFound, BadRequest).
- Validate that all input validation logic in controllers is covered by at least one test.

## Best Practices
- Use a mocking framework (e.g., Moq) to isolate controller logic from data access.
- Always use assert formatting for unit tests.
- Include tests for invalid, empty, null, and edge-case values.
- Test for case sensitivity, leading/trailing whitespace, and excessively long strings.
- Ensure tests cover both valid and invalid model states.

## Output
- List of missing edge case tests (if any)
- New or updated unit test code for each missing case
- Confirmation that all edge cases are now covered
