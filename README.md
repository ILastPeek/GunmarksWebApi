# GunmarksWebApi

**The web API for viewing the value for getting a mark on a gun in the World of Tanks game.**

Clients can filter, sort, and search tanks by nation, class, tier, status, and name. The API returns data in JSON format, making it suitable for mobile apps, SPAs, Telegram bots, or other services.

> **Educational project.** This is a reimplementation of the functionality of the original website — [poliroid.me/gunmarks](https://poliroid.me/gunmarks/). The goal is learning and practicing REST API development with ASP.NET Core, clean architecture, and Git.

## 🛠 Tech Stack

- **ASP.NET Core Web API** (.NET 8)
- **Entity Framework Core**
- **MS SQL Server**
- **Swagger (OpenAPI)** for documentation
- **REST, JSON**

## 📦 Features

- `GET /api/tanks` — list of tanks with filtering, sorting, and pagination
- `GET /api/tanks/{id}` — single tank by Id
- `POST /api/tanks` — create a new tank
- `PUT /api/tanks/{id}` — update a tank
- `DELETE /api/tanks/{id}` — delete a tank
- Filtering by nation, vehicle type, tier, status
- Search by name
- Sorting by 7 columns (tier, nation, type, name, three marks)
- Automatic input validation via Data Annotations
- Data storage in SQL Server

## 🚀 Getting Started

1. Clone the repository:
   ```
   git clone https://github.com/ILastPeek/GunmarksWebApi.git
   ```
2. Configure the connection string in `appsettings.json`.
3. Apply migrations (the database is created locally and initialized once on first launch):
   ```
   dotnet ef database update
   ```
   > If you use the Package Manager Console in Visual Studio, the command will be:
   > ```
   > Update-Database
   > ```
   > Both commands do the same thing — apply migrations to the database.
4. Run the project:
   ```
   dotnet run
   ```
5. Open Swagger to browse and test the endpoints:
   ```
   https://localhost:{port}/swagger
   ```

## 🏗 Architecture

The project is built on **clean layered architecture**. Each layer has its own responsibility and depends only on the layers below.

- **Domain** — domain entities (`Domain/Entities`) and enums (`Domain/Enums`). A pure domain model that knows nothing about the database or HTTP.
- **Data** — EF Core context (`Data/AppDbContext`) and database initializer (`Data/DbInitializer`). Responsible for database connectivity.
- **Repositories** — data access abstraction (`Repositories/Abstract` — interfaces, `Repositories/EntityFramework` — implementations).
- **Services** — business logic. Filtering, sorting, pagination, mapping Entity → DTO.
- **DTOs** — API contracts (Data Transfer Objects). Separate DTOs for reading, creating, and updating.
- **Controllers** — thin layer that accepts HTTP requests and calls services. Contains no business logic.
- **Helpers** — utility classes (for example, number-to-Roman conversion).

Data flow:
`HTTP request → Controller → Service → Repository → AppDbContext → SQL Server → Service → DTO → Controller → JSON response`.

## 🧭 Project Structure

```
GunmarksWebApi/
├── Controllers/          # API controllers
├── Data/                 # DbContext and DB initializer
├── Domain/
│   ├── Entities/         # Domain entities (Tank, Nation, TankType)
│   └── Enums/            # Enums (TankStatus)
├── DTOs/                 # API contracts
├── Helpers/              # Utility classes
├── Repositories/
│   ├── Abstract/         # Repository interfaces
│   └── EntityFramework/  # Repository implementations
├── Services/             # Business logic
├── Program.cs            # Entry point and DI configuration
└── appsettings.json      # Configuration
```

## 📝 Notes

- **Frontend** is fully absent. The project returns only JSON. Swagger is used for browsing and testing.
- **Database** is created locally. On first launch, the application initializes it with seed data (42 tanks, 12 nations, 5 vehicle types).
- **Validation** of input data works automatically thanks to the `[ApiController]` attribute and Data Annotations in DTOs.
- **REST principles** are followed: correct HTTP methods, response codes (200, 201, 204, 400, 404), idempotency of PUT and DELETE.

## 👤 Author

**ILastPeek** — [GitHub](https://github.com/ILastPeek)
