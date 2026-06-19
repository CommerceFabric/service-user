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

- Uses Dapper as the ORM (not EF)
    - While EF is better for change tracking (migrations), and has LINQ support
    - Dapper is better for high-performance latency-sensitive Microservices as can execute raw sql with minimal overhead and not relying on how LINQ is decomiled into sql
- Dependency Injection
- Uses AutoMapper
- Clean Architecture Principles (split sub-projects: API | Core | Infrastructure )
- Has injected Exception Handling Middleware
- Has Fluent Validation for confirming correctness of DTO's
- Uses Swagger for interactive API documentation