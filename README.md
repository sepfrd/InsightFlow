# **InsightFlow v2**

InsightFlow v2 is a **Clean Architecture**-based web application built with **.NET Web API**, providing a structured platform for users to manage blog posts. It allows users to register, authenticate, and perform CRUD operations on their blog posts while ensuring security and scalability.

## **Table of Contents**
- [Features](#features)
- [Technologies](#technologies)
- [Usage](#usage)
- [Rate Limiting](#rate-limiting)
- [Health Checks](#health-checks)

## **Features**

- **Clean Architecture**: Separation of concerns with Domain, Application, Infrastructure, and API layers.
- **User Authentication**: JWT-based authentication for secure access.
- **User Signup**: Everyone can create a user.
- **BlogPost Management**: Users can create, update, delete, and view their blog posts.
- **Integration & Unit Tests**: Ensuring correctness and stability.
- **Rate Limiting**: Fixed Window, Token Bucket, and Concurrency limiters to prevent abuse.
- **Health Checks**: Endpoint for monitoring service health.
- **OpenAPI Documentation**: Instead of Swagger, OpenAPI provides detailed API documentation.
- **Scalar UI**: Used for frontend visualization and API interaction.

## **Technologies**

- .NET Web API (with Clean Architecture)
- Entity Framework Core
- SQL Server (Dockerized)
- JWT Authentication
- Serilog for logging
- FluentValidation for request validation
- Mapster for entity mapping
- OpenAPI for API documentation
- Scalar UI for API visualization
- xUnit for unit tests
- Docker and Docker Compose

## **Getting Started**

### **Prerequisites**

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker](https://www.docker.com/get-started) (for containerized setup)

### **Installation**

1. Clone the repository:
   ```bash
   git clone https://github.com/sepfrd/InsightFlow.git
   cd InsightFlow
   ```  
2. Modify the `docker-compose.yml` file if needed (SQL Server password, application ports, etc.).

### **Usage**

1. Run the following command (set the **SQL Server SA password** via environment variables first):
   ```bash
   MSSQL_SA_PASSWORD=SOME_STRONG_PASSWORD_HERE docker compose --file docker-compose.yaml up
   ```  
2. Access the application:
    - API: `http://localhost:8000`
    - Database: `http://localhost:1533` (or custom port from `docker-compose.yml`)

## **Rate Limiting**

InsightFlow v2 implements multiple rate-limiting strategies to handle incoming requests efficiently:

- **Fixed Window Rate Limiter**: Limits requests per time window.
- **Token Bucket Rate Limiter**: Allows burst requests up to a maximum token count.
- **Concurrency Limiter**: Controls concurrent requests to prevent server overload.

Rate limit configurations can be adjusted in `appsettings.json`, `docker-compose.yml` or set via environment variables.

## **Health Checks**

A health check endpoint is provided for system monitoring:

- **Endpoint**: `/health`  
