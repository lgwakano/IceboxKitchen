# IceboxKitchen

IceboxKitchen is a robust, modular web application built with Clean Architecture principles. This project serves as a practical implementation of separating business logic from external concerns, ensuring high maintainability, testability, and scalability.

## 🏗️ Architecture Overview

This project follows a clean, decoupled architecture to ensure that the core business rules are independent of frameworks and external dependencies.

- `IceboxKitchen.Domain`: Contains the core entities, value objects, and domain logic. This is the heart of the application and has no dependencies on other layers.
- `IceboxKitchen.Application`: Defines the application use cases and interfaces. It depends only on the `Domain` layer.
- `IceboxKitchen.Infrastructure`: Handles external concerns such as database access (Entity Framework Core, planned), identity management, and external API integrations.
- `IceboxKitchen.Contracts`: Houses Data Transfer Objects (DTOs) and API specifications, ensuring a clean contract between the client and the server.
- `IceboxKitchen.Api`: The entry point for the application, exposing HTTP endpoints and orchestrating the flow of requests.

## 🚀 Key Features

- **Modular Design**: Clearly separated concerns using C#.
- **Robust Authentication**: Implements a complete user login and authentication workflow.
- **API-First Approach**: Built with clear contract definitions for seamless frontend integration.
- **Maintainability**: Designed for easy extension as new kitchen/food management features are added.

## 🛠️ Tech Stack

- **Language**: C#
- **Framework**: .NET 10
- **Architecture**: Clean Architecture / DDD patterns
- **Data Access**: Entity Framework Core (planned)

## 📦 Project Structure

```text
/
├── Docs/                          # Project documentation
├── IceboxKitchen.Api/             # Web API entry point
├── IceboxKitchen.Application/     # Business logic & use cases
├── IceboxKitchen.Contracts/       # DTOs & API models
├── IceboxKitchen.Domain/          # Core entities & rules
├── IceboxKitchen.Infrastructure/  # Persistence & external services
├── Requests/                      # API request examples
└── IceboxKitchen.slnx             # Solution file
```

## ▶️ Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

### Setup

```bash
# Clone the repository
git clone https://github.com/lgwakano/IceboxKitchen.git
cd IceboxKitchen

# Restore dependencies
dotnet restore

# Run the API
dotnet run --project IceboxKitchen.Api
```

> Note: A database has not been wired up yet — data access is still on the roadmap. Setup steps will be expanded once persistence is in place.

## 📈 Status & Roadmap

This project is a learning-driven, work-in-progress build (currently on milestone 3 of 19). It follows a structured, step-by-step approach to Clean Architecture and Domain-Driven Design in .NET, moving from project scaffolding through authentication, CQRS, domain modeling, and persistence.

- [x] 1. Solution & project scaffolding
- [x] 2. Authentication & JWT token generation
- [x] 3. Repository pattern
- [x] 4. Global error handling
- [x] 5. Flow control & result patterns
- [ ] 6. CQRS with MediatR
- [ ] 7. Object mapping (Mapster)
- [ ] 8. Request validation & pipeline behaviors
- [ ] 9. JWT bearer authentication & authorization
- [ ] 10. Process modeling
- [ ] 11. Domain modeling (aggregates & bounded contexts)
- [ ] 12. Aggregate root, entity & value object base classes
- [ ] 13. Domain layer structure
- [ ] 14. Wiring Clean Architecture + DDD + CQRS end-to-end
- [ ] 15. Entity Framework Core & DDD persistence mapping
- [ ] 16. Automated testing (unit & integration tests)
- [ ] 17–19. Remaining milestones (to be refined as the project progresses)

> This roadmap reflects the learning path being followed, adapted to this project's own domain (kitchen/food inventory management) rather than a copy of any external source.

## 📄 License

_Add license information here (e.g. MIT, Apache 2.0)._

---

Built with ❤️ by [Luiz Wakano](https://github.com/lgwakano)
