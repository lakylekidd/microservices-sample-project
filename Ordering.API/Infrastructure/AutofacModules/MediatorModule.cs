using Autofac;
using MediatR;
using System.Reflection;

namespace Ordering.API.Infrastructure.AutofacModules
{
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Register MediatR with Autofac
            builder.RegisterAssemblyModules(typeof(IMediator).GetTypeInfo().Assembly);
        }
    }
}
