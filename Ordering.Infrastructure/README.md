# Ordering Infrastruction

The ordering infrastruction is responsible for defining the entity and database configurations for the microservice.
In short, this project is responsible for the following configurations:

- Entity Configurations
- Idempotency
- Repository Implementations
- Database Context Implementation

## Project Packages

For this project we will only be using a set of packages that will define the structure and technology of our microservice's database.

- [MediatR 7.0.0](https://github.com/jbogard/mediatr) by Jimmy Bogard
- [Microsoft.EntityFrameWorkCore](https://github.com/aspnet/EntityFrameworkCore) by Dotnet
- [Microsoft.EntityFrameWorkCore.SqlServer](https://github.com/aspnet/EntityFrameworkCore) by Dotnet

## Project Contents

In this section we will describe what each of the project's content is responsible for.

### Entity Configurations

Entity Framework Fluent API is used to configure domain classes to ovverride conventions. EF Fluent API is based on Fluent API design pattern where the result is formulated by method chaining.

In Entity Framework 6, the DbModelBuilder class acts as a Fluent API using which we can configure many different things. It provides more options of configurations than Data Annotation attributes.

Entity Framework allows you to create a separate class for each entity and place all the configurations related to an entity.