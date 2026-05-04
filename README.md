## 🛍️ FashionStore API

A backend API for managing a fashion store system, built with Clean Architecture and .NET 9.
The project is structured for scalability, maintainability, and testability.

## 🚀 Tech Stack

- **.NET 9**
- **Entity Framework Core** – ORM for database access
- **CQRS** – Separate read & write logic
- **FluentValidation** – Request validation
- **Mapster** – Lightweight object mapping
- **xUnit** – Unit testing
- **Serilog** – Structured logging
- **Azure Blob Storage** – Cloud storage for files and product images
- **Azure App Service** – Scalable PaaS for web hosting

## 📂 Architecture & Project Structure

This project follows **Clean Architecture**, organizing code into layers with clear responsibilities and dependencies:

- API → Application → Domain
- Infrastructure → Application
- Domain: no dependency

<pre>
src
├── FashionStore.API/                # Presentation Layer (Entry point)
│   ├── Controllers/                 
│   ├── Middlewares/                 
│   └── Extensions/                  # Helpers, extensions

├── FashionStore.Application/        # Application Layer (Business logic, CQRS)
│   ├── Features/                    # CQRS Handlers
│   │   └── Users/
│   │       ├── Commands/            # Write operations
│   │       └── Queries/             # Read operations
│   ├── Interfaces/                  # Abstractions (IRepository)
│   └── Common/
│       ├── DTOs/                    # Data Transfer Objects
│       ├── Mappings/                # Mapster config
│       ├── Enums/                   # Enums
│       └── Behaviors/               # MediatR Pipeline Behaviors (Validation, Logging)

├── FashionStore.Domain/             # Domain Layer (Core business)
│   ├── Entities/                    # Domain entities
│   ├── Enums/                       # Domain Enums
│   └── ValueObjects/                # Value Objects (DDD)

└── FashionStore.Infrastructure/     # Infrastructure Layer
│   ├── Persistence/
│   │   ├── Repositories/            # Implementation of repository interfaces
│   │   ├── Configurations/          # EF Core Fluent API configs
│   │   └── Migrations/              # EF Core migrations
│   ├── Authentication/
│   └── Services/                    # External services (SMS, Email, Third-party APIs)

test/
  ├── Domain.Tests/
  ├── Application.Tests/
  └── Infrastructure.Tests/

</pre>

## ⚙️ Getting Started

### 1. Build the Solution

- From the root directory containing your .sln file, run:

```
dotnet build
```

### 2. Run project

```
cd src/FashionStore.API
dotnet run
```
## 📚 API Reference

* **Development:** [Link Swagger](https://fashionstoreapi29-c0c0e9h2eja6cbc6.centralus-01.azurewebsites.net/swagger/index.html)
