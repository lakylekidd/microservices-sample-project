using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microservices.Library.IntegrationEventLogEF;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
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

            // Build our host using the web host builder
            var hostBuilder = new WebHostBuilder()
                .UseContentRoot(Path.GetDirectoryName(path))
                .ConfigureAppConfiguration(cb =>
                {
                    cb.AddJsonFile("appsettings.json", optional: false)
                    .AddEnvironmentVariables();
                }).UseStartup<OrderingTestsStartup>();

            // Create an instance of our test server using the host builder
            var testServer = new TestServer(hostBuilder);

            // Migrate the test ordering context
            testServer.Host
                .MigrateDbContext<OrderingContext>((context, services) =>
                {
                    var env = services.GetService<IHostingEnvironment>();
                    var settings = services.GetService<IOptions<OrderingSettings>>();
                    var logger = services.GetService<ILogger<OrderingContextSeed>>();

                    //var seedContext = new OrderingContextSeed();

                    //seedContext.SeedAsync(context, env, settings, logger).Wait();

                    new OrderingContextSeed()
                        .SeedAsync(context, env, settings, logger)
                        .Wait();
                })
                .MigrateDbContext<IntegrationEventLogContext>((_, __) => { });

            // Return the test server
            return testServer;
        }
    }
}
