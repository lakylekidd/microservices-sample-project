using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Ordering.API;
using Ordering.FunctionalTests.Helpers;

namespace Ordering.FunctionalTests
{
    public abstract class IntegrationTest
    {
        /// <summary>
        /// An idempotent test http client used for testing your app
        /// </summary>
        protected readonly HttpClient TestIdempotentHttpClient;
        /// <summary>
        /// A test http client used for testing yoru app
        /// </summary>
        protected readonly HttpClient TestHttpClient;

        /// <summary>
        /// The test server
        /// </summary>
        protected readonly TestServer TestServer;

        protected IntegrationTest()
        {
            // Create the web application factory
            var appFactory = new CustomWebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    // At this point of our code, all the services have already been configured
                    // In this section we can reconfigure some of them.
                    builder.ConfigureServices(services =>
                    {
                        // Reconfigure services here
                    });
                });
            // Create the test http client
            TestHttpClient = appFactory.CreateClient();
            TestIdempotentHttpClient = appFactory.Server.CreateIdempotentClient();
            // Create the test server
            TestServer = appFactory.Server;
        }
    }
}
