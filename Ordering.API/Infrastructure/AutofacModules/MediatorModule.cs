using Autofac;
using MediatR;
using System.Reflection;

namespace Ordering.API.Infrastructure.AutofacModules
{
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Retrieve the assembly information of the current project
            var assembly = typeof(Startup).GetTypeInfo().Assembly;

            // Register MediatR with Autofac
            builder.RegisterAssemblyModules(typeof(IMediator).GetTypeInfo().Assembly);

            // Register all the classes that implement the following
            // interfaces and that reside in the current assembly.
            // ATTN: If we decide to change the location of these classes (commands and domain events)
            // Then we should consider scanning in the new assembly they will reside.
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>))        // Register command classes that implement IRequestHandler
                .AsClosedTypesOf(typeof(INotificationHandler<>));   // Register domain event classes that implement INotificationHandler
        }
    }
}
