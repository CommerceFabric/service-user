# Requirements

- PostgresSQL 
    - Seeding (currently manual, soon will be replaced - probably in docker?)
        - Create a database called eCommerceUsers 
        - Create a table using:
        
```sql
create table users (
    user_id uuid primary key,
    person_name varchar(50) not null,
    email varchar(50) not null,
    password varchar(50) not null,
    gender varchar(50) not null
);
```


# Technical INFO

- Uses Dapper as the ORM (not EF)
- Dependency Injection
- Uses AutoMapper
- Clean Architecture Principles (split sub-projects: API | Core | Infrastructure )
- Has injected Exception Handling Middleware
- Has Fluent Validation for confirming correctness of DTO's
- Uses Swagger for API

# Next steps:

## code improvements
- seed the DB's automatically
- add in Outbox pattern + Saga workflow
- add in Idemptotency
- add in a proper Observability Layer (eg Serilog structured logs + correlation IDs) 

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