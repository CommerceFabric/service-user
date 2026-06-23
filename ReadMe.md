# Requirements

- [PostgresSQL](https://www.enterprisedb.com/downloads/postgres-postgresql-downloads) 18.0.0 or higher
    - Seeding- must be done manually as we are using Dapper

- Use pgAdmin to create a new Database called CommerceFabricUsers, then populate it using the below query.
```sql
CREATE TABLE users (
    userid UUID PRIMARY KEY,
    personname VARCHAR(50) NOT NULL,
    email VARCHAR(50) NOT NULL,
    password VARCHAR(50) NOT NULL,
    gender VARCHAR(50) NOT NULL,
    CONSTRAINT email_unique UNIQUE (email)
);
```

- After doing so, populate using secrets.json in visualStudio with the connection string for that Db.
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=userService;User Id=postgres;Password=yourPasswordHere!;"
  }
}
```

# Technical Info

## Architecture

- Uses the Clean Architecture (API / Core / Infrastructure) pattern, which keeps business rules independent of frameworks, databases, and external services.
    - API – Exposes endpoints, handles requests/responses, authentication, and application configuration.
    - Core – Contains domain models, business rules, use cases, and contracts/interfaces for external dependencies.
    - Infrastructure – Implements persistence, external integrations, and other technical concerns defined by the Core.

- The Layered Architecture is an alternative approach that organizes the application into layers (Presentation, Business Logic, Data Access), this is done to separate concerns and promote maintainability, but it can lead to tight coupling between layers and less flexibility in adapting to changes.
    - I implemented the [Product Service](https://github.com/CommerceFabric/service-products) Micro-Service using this approach to demonstrate the differences between the two architectures and how Clean Architecture can provide better separation of concerns and flexibility in adapting to changes.


## Technical Stack

- Uses Dapper as the ORM (not EF)
    - While EF is better for change tracking (migrations), and has LINQ support
    - Dapper is better for high-performance latency-sensitive Microservices as can execute raw sql with minimal overhead and not relying on how LINQ is decomiled into sql
- Dependency Injection
- Uses AutoMapper
- Clean Architecture Principles (split sub-projects: API | Core | Infrastructure )
- Has injected Exception Handling Middleware
- Has Fluent Validation for confirming correctness of DTO's, automatically validated incoming requests through FluentValidation's MVC Pipeline Middleware
- Uses Swagger for interactive API documentation
- Uses PostgresSQL as the database
- Uses traditional Controller-based endpoints
