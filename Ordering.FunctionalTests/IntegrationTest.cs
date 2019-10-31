using System.Net.Http;
using Microservices.Library.IntegrationEventLogEF;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Ordering.Infrastructure;

namespace Ordering.FunctionalTests
{
    public abstract class IntegrationTest
    {
        protected readonly HttpClient TestHttpClient;

        protected IntegrationTest()
        {
            // Create the web application factory
            var appFactory = new WebApplicationFactory<OrderingTestsStartup>()
                .WithWebHostBuilder(builder =>
                {
                    // At this point of our code, all the services have already been configured
                    // In this section we can reconfigure some of them.
                    builder.ConfigureServices(services =>
                    {
                        // Remove all instances of db contexts and 
                        // replace them with the in-memory ones
                        services.RemoveAll(typeof(IntegrationEventLogContext));
                        services.RemoveAll(typeof(OrderingContext));
                        // Add in-memory contexts
                        services
                            .AddDbContext<OrderingContext>(options =>
                            {
                                options.UseInMemoryDatabase("OrderingTestDB");
                                // Configure to ignore any warnings
                                options.ConfigureWarnings(warningBuilder =>
                                {
                                    warningBuilder.Ignore(InMemoryEventId.TransactionIgnoredWarning);
                                });
                            })
                            .AddDbContext<IntegrationEventLogContext>(options =>
                            {
                                options.UseInMemoryDatabase("IntegrationEventLogDB");
                                // Configure to ignore any warnings
                                options.ConfigureWarnings(warningBuilder =>
                                {
                                    warningBuilder.Ignore(InMemoryEventId.TransactionIgnoredWarning);
                                });
                            });
                    });
                });
            // Create the test http client
            TestHttpClient = appFactory.CreateClient();
        }
    }
}
