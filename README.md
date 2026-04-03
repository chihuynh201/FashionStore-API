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
│   ├── Features/                    # Feature-based structure
│   │   └── Users/
│   │       ├── Commands/            # Write operations
│   │       └── Queries/             # Read operations
│   ├── Interfaces/                  # Abstractions
│   └── Common/
│       ├── DTOs/                    # Request/Response models
│       ├── Mappings/                # Mapster config
│       ├── Enums/                   # Enums
│       └── Behaviors/               # Pipeline (Validation, Logging)

├── FashionStore.Domain/             # Domain Layer (Core business)
│   ├── Entities/                    # Domain entities
│   ├── Enums/                       # Enums
│   └── ValueObjects/                # Value Objects (DDD)

└── FashionStore.Infrastructure/     # Infrastructure Layer
│   ├── Persistence/
│   │   ├── Repositories/            # Implementation of repository interfaces
│   │   ├── Configurations/          # EF Core Fluent API configs
│   │   └── Migrations/              # EF Core migrations
│   ├── Authentication/
│   └── Services/                    # External services (SMS, Email, Third-party APIs)

test/
└── Tests                            # Test project
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
