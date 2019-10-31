using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microservices.Library.IntegrationEventLogEF;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ordering.API;
using Ordering.API.Infrastructure;
using Ordering.Infrastructure;
using WebHost.Customization;
using Microsoft.Extensions.DependencyInjection;


namespace Ordering.FunctionalTests
{
    public class OrderingScenarioBase
    {
        /// <summary>
        /// Method that creates a new test server for our application
        /// </summary>
        /// <returns></returns>
        public TestServer CreateServer()
        {
            // Retrieve the path of our assembly
            var path = Assembly.GetAssembly(typeof(OrderingScenarioBase))
                .Location;

            var hostBuilder = new WebHostBuilder()
                .UseContentRoot(Path.GetDirectoryName(path))
                .ConfigureAppConfiguration(cb =>
                {
                    cb.AddJsonFile("appsettings.json", optional: false)
                        .AddEnvironmentVariables();
                })
                //.ConfigureServices(services =>
                //{
                //    services.AddDbContext<OrderingContext>(options =>
                //    {
                //        options.ConfigureWarnings(builder => builder.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                //        options.UseInMemoryDatabase(Guid.NewGuid().ToString());
                //    });
                //    services.AddDbContext<IntegrationEventLogContext>(options =>
                //    {
                //        options.ConfigureWarnings(builder => builder.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                //        options.UseInMemoryDatabase(Guid.NewGuid().ToString());
                //    });
                //})
                .UseStartup<OrderingTestsStartup>();
            
            // Create an instance of our test server using the host builder
            var testServer = new TestServer(hostBuilder);

            testServer.Host.MigrateDbContext<OrderingContext>((context, services) =>
                {
                })
                .MigrateDbContext<IntegrationEventLogContext>((_, __) => { });

            // Return the test server
            return testServer;
        }
    }
}
