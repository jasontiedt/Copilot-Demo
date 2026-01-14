# C# Spring Boot Converted Project

This is a converted version of the Java Spring Boot Contacts application, rewritten in C# using ASP.NET Core best practices.

## Structure
- Controllers
- Models
- Data (for DbContext and Repositories)
- appsettings.json (configuration)
- Program.cs (entry point)
- Startup.cs (service configuration)

## Notes
- Follows C# naming conventions (PascalCase for classes, properties, methods)
- Uses dependency injection for DbContext and repositories
- Uses Entity Framework Core for data access
- OpenAPI/Swagger enabled

---

To run:
1. Ensure .NET 8.0 SDK is installed.
2. `dotnet restore`
3. `dotnet build`
4. `dotnet run`

---

This project is a direct conversion and may require further adjustments for production use.