using Autofac;
using MediatR;
using System.Reflection;
using FluentValidation;
using Ordering.API.Application.Behaviors;
using Ordering.API.Application.Commands;
using Ordering.API.Application.CommandValidations;

namespace Ordering.API.Infrastructure.AutofacModules
{
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Retrieve the assembly information of the current project
            var assembly = typeof(Startup).GetTypeInfo().Assembly;

            //// Register MediatR with Autofac
            ////builder.RegisterAssemblyModules(typeof(IMediator).GetTypeInfo().Assembly);

            //builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
            //    .AsImplementedInterfaces();

            //// Register all the classes that implement the following
            //// interfaces and that reside in the current assembly.
            //// ATTN: If we decide to change the location of these classes (commands and domain events)
            //// Then we should consider scanning in the new assembly they will reside.
            //builder.RegisterAssemblyTypes(assembly)
            //    .AsClosedTypesOf(typeof(IRequestHandler<,>))        // Register command classes that implement IRequestHandler
            //    .AsClosedTypesOf(typeof(INotificationHandler<>));   // Register domain event classes that implement INotificationHandler

            //// Register the Command's Validators (Validators based on FluentValidation library)
            //builder.RegisterAssemblyTypes(assembly)
            //    .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
            //    .AsImplementedInterfaces();

            //builder.Register<ServiceFactory>(context =>
            //{
            //    var componentContext = context.Resolve<IComponentContext>();
            //    return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            //});

            //builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            //builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            //builder.RegisterGeneric(typeof(TransactionBehaviour<,>)).As(typeof(IPipelineBehavior<,>));

            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            // Register all the Command classes (they implement IRequestHandler) in assembly holding the Commands
            builder.RegisterAssemblyTypes(typeof(CreateOrderCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            // Register the DomainEventHandler classes (they implement INotificationHandler<>) in assembly holding the Domain Events
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(INotificationHandler<>));

            // Register the Command's Validators (Validators based on FluentValidation library)
            builder
                .RegisterAssemblyTypes(typeof(CreateOrderCommandValidator).GetTypeInfo().Assembly)
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces();


            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            });

            builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(TransactionBehaviour<,>)).As(typeof(IPipelineBehavior<,>));
        }
    }
}
