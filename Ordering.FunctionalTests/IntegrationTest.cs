using System.Net.Http;
using Microservices.Library.EventBus;
using Microservices.Library.EventBus.Abstractions;
using Microservices.Library.EventBusRabbitMQ;
using Microservices.Library.IntegrationEventLogEF;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Ordering.API;
using Ordering.API.Application.Behaviors;
using Ordering.FunctionalTests.Helpers;
using Ordering.Infrastructure;

namespace Ordering.FunctionalTests
{
    public abstract class IntegrationTest
    {
        /// <summary>
        /// An idempotent test http client used for testing your app
        /// </summary>
        protected readonly HttpClient TestIdempotentHttpClient;
        /// <summary>
        /// A test http client used for testing your app
        /// </summary>
        protected readonly HttpClient TestHttpClient;

        /// <summary>
        /// The test server
        /// </summary>
        protected readonly TestServer TestServer;

        protected IntegrationTest()
        {
            //// Create the web application factory
            //var appFactory = new CustomWebApplicationFactory<Startup>()
            //    .WithWebHostBuilder(builder =>
            //    {
            //        // At this point of our code, all the services have already been configured
            //        // In this section we can reconfigure some of them.
            //        builder.ConfigureServices(services =>
            //        {
            //            // Remove all instances of db contexts and 
            //            // replace them with the in-memory ones
            //            services.RemoveAll(typeof(IntegrationEventLogContext));
            //            services.RemoveAll(typeof(OrderingContext));

            //            // Disable the use of transactional behaviour
            //            services.RemoveAll(typeof(TransactionBehaviour<,>));
            //            services.RemoveAll(typeof(IEventBus));
            //            services.RemoveAll(typeof(IEventBusSubscriptionsManager));
            //            services.RemoveAll(typeof(IRabbitMQPersistentConnection));

            //            // Add in-memory contexts
            //            services
            //                .AddDbContext<OrderingContext>(options =>
            //                {
            //                    options.UseInMemoryDatabase("OrderingTestDB");
            //                    // Configure to ignore any warnings
            //                    options.ConfigureWarnings(warningBuilder =>
            //                    {
            //                        warningBuilder.Ignore(InMemoryEventId.TransactionIgnoredWarning);
            //                    });
            //                })
            //                .AddDbContext<IntegrationEventLogContext>(options =>
            //                {
            //                    options.UseInMemoryDatabase("IntegrationEventLogTestDB");
            //                    // Configure to ignore any warnings
            //                    options.ConfigureWarnings(warningBuilder =>
            //                    {
            //                        warningBuilder.Ignore(InMemoryEventId.TransactionIgnoredWarning);
            //                    });
            //                });
            //        });
            //    });
            //// Create the test http client
            //TestHttpClient = appFactory.CreateClient();
            //TestIdempotentHttpClient = appFactory.Server.CreateIdempotentClient();
            //// Create the test server
            //TestServer = appFactory.Server;
        }
    }
}
