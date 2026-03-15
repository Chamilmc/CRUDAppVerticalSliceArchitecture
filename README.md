🚀 CRUD App with Vertical Slice Architecture in .NET 10

This project demonstrates how to build a modern CRUD API using Vertical Slice Architecture in .NET 10.
It shows how to structure a maintainable and scalable backend by organizing code around features instead of layers.

The project also integrates Entity Framework Core, MediatR, and Scalar UI for testing APIs.

📌 Features

CRUD operations for Games
Vertical Slice Architecture
Entity Framework Core for data access
MediatR for request/response handling
Scalar UI for API testing
Clean and maintainable feature-based structure

🏗 Architecture

This project follows the Vertical Slice Architecture, where each feature contains everything it needs:

Request
Handler
Validation
Endpoint
Data logic
Instead of separating code into traditional layers like Controllers, Services, and Repositories, each feature is self-contained.

Example structure:

src
 ├── Features
 │   └── Games
 │        ├── CreateGame
 │        │    ├── CreateGameCommand.cs
 │        │    ├── CreateGameHandler.cs
 │        │    └── CreateGameEndpoint.cs
 │        │
 │        ├── GetAllGames
 │        │    ├── GetAllGamesQuery.cs
 │        │    ├── GetAllGamesHandler.cs
 │        │    └── GetAllGamesEndpoint.cs
 │        │
 │        ├── UpdateGame
 │        │
 │        └── DeleteGame
 │
 ├── Data
 │   └── AppDbContext.cs
 │
 └── Program.cs
 
⚙️ Technologies Used
.NET 10
Entity Framework Core
MediatR
Minimal APIs
Scalar UI
