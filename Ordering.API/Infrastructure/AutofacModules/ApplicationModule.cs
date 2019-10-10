using Autofac;

using App.Services.Ordering.Infrastructure.Repositories;
using Ordering.Domain.AggregatesModel.BuyerAggregate;
using Ordering.Domain.AggregatesModel.OrderAggregate;
using App.Services.Ordering.Infrastructure.Idempotency;

namespace Ordering.API.Infrastructure.AutofacModules
{
    /// <summary>
    /// This module is responsible for mapping any service/repository implementation to their corresponding interfaces.
    /// </summary>
    public class ApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BuyerRepository>()
                .As<IBuyerRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<OrderRepository>()
                .As<IOrderRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RequestManager>()
               .As<IRequestManager>()
               .InstancePerLifetimeScope();
        }
    }
}
