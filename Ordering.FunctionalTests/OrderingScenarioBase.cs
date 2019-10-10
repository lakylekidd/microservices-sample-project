using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace Ordering.FunctionalTests
{
    public class OrderingScenarioBase
    {
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

            // Return the test server
            return testServer;
        }
    }
}
