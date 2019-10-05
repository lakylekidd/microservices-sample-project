# Ordering Domain

This is the ordering domain project of the Ordering microservice. Notice how the naming convention is kept 
simple so that we can easily identify which microservice, as well as which part of the microservice we 
are currently in.

## Nuget Packages Used

The Domain is a very low-level project, so it is, in its base, a very simple project. For this, however,
we will need to include the following nuget Packages:

- [MediatR 7.0.0](https://github.com/jbogard/mediatr) by Jimmy Bogard
- [MediatR.Extensions.Microsoft.DependencyInjection 7.0.0](https://github.com/jbogard/MediatR.Extensions.Microsoft.DependencyInjection)
- [System.Reflection.TypeExtensions 4.6.0](https://github.com/dotnet/corefx) by Dotnet