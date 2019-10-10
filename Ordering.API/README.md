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

## Using MediatR

MediatR is a very useful tool that allow us to handle and deliver commands and events throughout our domain. 
In this project we are configuring MediatR inside the `Infrastructure/AutofacModules` folder under the `MediatorModule.cs` file. 

In this file we are registering some types MediatR is exposing in order to route the commands and events being sent by the domain. The following 
is a list of types we will be registering in our container:

- **`IRequestHandler`** : Classes that implement this interface are defined as Commands.
- **`INotificationHandler`** : Classes that implement this interface are defined as Domain Events.