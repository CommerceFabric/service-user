# Requirements

- PostgresSQL 
    - Seeding- must be done manually as we are using Dapper
        
```sql
CREATE TABLE users (
    user_id UUID PRIMARY KEY,
    person_name VARCHAR(50) NOT NULL,
    email VARCHAR(50) NOT NULL,
    password VARCHAR(50) NOT NULL,
    gender VARCHAR(50) NOT NULL,
    CONSTRAINT email_unique UNIQUE (email)
);
```

# Technical INFO

## Architecture

- Uses the Clean Architecture (API / Core / Infrastructure) pattern, which keeps business rules independent of frameworks, databases, and external services.
    - API – Exposes endpoints, handles requests/responses, authentication, and application configuration.
    - Core – Contains domain models, business rules, use cases, and contracts/interfaces for external dependencies.
    - Infrastructure – Implements persistence, external integrations, and other technical concerns defined by the Core.

## Technical Stack

- Uses Dapper as the ORM (not EF)
    - While EF is better for change tracking (migrations), and has LINQ support
    - Dapper is better for high-performance latency-sensitive Microservices as can execute raw sql with minimal overhead and not relying on how LINQ is decomiled into sql
- Dependency Injection
- Uses AutoMapper
- Clean Architecture Principles (split sub-projects: API | Core | Infrastructure )
- Has injected Exception Handling Middleware
- Has Fluent Validation for confirming correctness of DTO's
- Uses Swagger for interactive API documentation