# InsightFlow

InsightFlow is a Q&A web application built with .NET Web API, designed to facilitate the posting, answering, and voting on questions. This project is designed with a layered architecture, with a focus on clean and modular code.

## Table of Contents

- [Features](#features)
- [Technologies](#technologies)
- [Usage](#usage)
- [Rate Limiting](#rate-limiting)
- [Health Checks](#health-checks)

## Features

- **User Authentication**: JWT-based authentication for secure login and access.
- **Rate Limiting**: Multiple rate limiting strategies to prevent abuse.
- **Entity Management**: CRUD operations for questions, answers, and user profiles.
- **Voting System**: Users can upvote or downvote responses.
- **Health Checks**: Regular health checks for monitoring service health.
- **Swagger Documentation**: Interactive API documentation via Swagger.

## Technologies

- .NET Web API
- Entity Framework Core
- SQL Server (Dockerized)
- JWT Authentication
- Serilog for logging
- FluentValidation for request validation
- Rate limiting with Fixed Window and Token Bucket algorithms
- Docker and Docker Compose

## Getting Started

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker](https://www.docker.com/get-started) (for containerized setup)

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/InsightFlow.git
   cd InsightFlow
   ```
2. Modify the docker-compose.yml file if needed (SQL Server password, application ports, etc.)

### Usage

1. Run the following command (you have to set up Microsoft SQL Server SA password via environment variables first):
   ```bash
   MSSQL_SA_PASSWORD=SOME_STRONG_PASSWORD_HERE docker compose --file docker-compose.yaml up
   ```
2. Now you can access the application at http://localhost:8000 and the Microsoft SQL Server database instance at http://localhost:1533 (or any other port you specified in previous steps).

### Rate Limiting

InsightFlow uses three types of rate limiters to manage and limit user requests:

- Fixed Window Rate Limiter: Allows a fixed number of requests per window of time.
- Token Bucket Rate Limiter: Refills tokens periodically to allow for burst handling.
- Concurrency Limiter: Limits concurrent requests to manage load on the application.

Configuration options are available in appsettings.json and can be set via environment variables in docker-compose.yml.

### Health Checks

The application provides a health check endpoint for monitoring system health:
- Endpoint: /health