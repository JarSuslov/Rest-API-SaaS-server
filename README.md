# TodoSaaS — Task Management REST API

## Tech Stack

- C# 12 / .NET 8
- ASP.NET Core Web API
- PostgreSQL 16 + Entity Framework Core (Code First)
- JWT Authentication
- Rate Limiting (built-in `Microsoft.AspNetCore.RateLimiting`)
- Docker + Docker Compose

## Project Structure

```
├── Controllers/          # API controllers (Auth, Tasks, Admin)
├── Models/               # Entities (User, TodoTask)
├── DTOs/                 # Data Transfer Objects (requests/responses)
│   ├── Auth/
│   └── Tasks/
├── Services/             # Business logic
│   └── Interfaces/
├── Data/                 # DbContext + migrations
├── Common/               # Middleware, Extensions
│   ├── Middleware/
│   └── Extensions/
├── Program.cs            # Entry point
├── appsettings.json      # Configuration
├── Dockerfile            # Multistage build
└── docker-compose.yml    # Orchestration (app + postgres)
```

## Running with Docker Compose

```bash
docker-compose up --build
```

API will be available at: `http://localhost:8080`  
Swagger UI: `http://localhost:8080/swagger`

## Running Locally (without Docker)

1. Make sure PostgreSQL is running on `localhost:5432`.
2. Set User Secrets for the JWT key:

```bash
dotnet user-secrets set "Jwt:Key" "YourSuperSecretKeyMinimum32Characters!!"
```

3. Run:

```bash
dotnet run
```

## Endpoints

| Method | URL                  | Description                          | Authorization        |
|--------|----------------------|--------------------------------------|---------------------|
| POST   | /api/auth/register   | Register a new user                  | —                   |
| POST   | /api/auth/login      | Log in and receive a JWT             | —                   |
| GET    | /api/tasks           | Get all tasks for the current user   | Bearer Token        |
| GET    | /api/tasks/{id}      | Get a task by ID (own tasks only)    | Bearer Token        |
| POST   | /api/tasks           | Create a task                        | Bearer Token        |
| PUT    | /api/tasks/{id}      | Update a task                        | Bearer Token        |
| DELETE | /api/tasks/{id}      | Delete a task                        | Bearer Token        |
| GET    | /api/admin/tasks     | Get all tasks from all users         | Bearer Token (Admin)|

## Usage Examples

### Register

```bash
curl -X POST http://localhost:8080/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{"email": "user@example.com", "password": "password123"}'
```

### Login

```bash
curl -X POST http://localhost:8080/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email": "user@example.com", "password": "password123"}'
```

### Create a Task

```bash
curl -X POST http://localhost:8080/api/tasks \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer <TOKEN>" \
  -d '{"title": "My first task", "description": "Task description"}'
```

## Environment Variables (for Docker)

| Variable | Description |
|----------|-------------|
| `ConnectionStrings__DefaultConnection` | PostgreSQL connection string |
| `Jwt__Key` | JWT secret key (minimum 32 characters) |
| `Jwt__Issuer` | Token issuer |
| `Jwt__Audience` | Token audience |
| `Jwt__ExpireHours` | Token lifetime in hours |
