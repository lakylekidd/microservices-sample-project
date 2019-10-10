# Ordering API

This is the API endpoint of the Ordering Microservice.

## Nuget Packages Used

- [MediatR 7.0.0](https://github.com/jbogard/MediatR.Extensions.Microsoft.DependencyInjection)
- [Autofac.Extensions.DependencyInjection 5.1.0](https://autofac.org/)
- [MediatR.Extensions.Microsoft.DependencyInjection 7.0.0](https://github.com/jbogard/MediatR.Extensions.Microsoft.DependencyInjection)

## Dependency Injection

In order to be able to use certain services using their interfaces, we will be using MediatR to register them in the container. 
This implementation can be found inside the `Infrastructure/AutofacModules` folder under the `ApplicationModule.cs` file.

In these modules we are registering their mappings and introduce them into the new DI mechanism of our Application. We then use autofac
as the new service provider inside the `Startup.cs` file under the `ConfigureServices` function.