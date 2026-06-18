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

# Next steps:

## code improvements
- seed the DB's automatically
- add in Outbox pattern + Saga workflow
- add in Idemptotency
- add in a proper Observability Layer (eg Serilog structured logs + correlation IDs) 
- maybe do CQRS with MediatR?

## additional repos
- maybe also add in an admin page or similar which lets you health check all the different microservices etc. + view logs
    - not necessarily our own one, maybe an existing one eg Grafana or Aspire?
- maybe also replace the placeholder simple angular website with a nice looking blazor or react one?

## scaling

- add in ability to scale up/down each different microservice 
- add in ability to route properly if there are multiple instances of each microservice, so communication between them is load balanced


## testing

- add in some load testing + performance engineering
- add in chaos testing by intentionally breaking different microservice sor reliant components