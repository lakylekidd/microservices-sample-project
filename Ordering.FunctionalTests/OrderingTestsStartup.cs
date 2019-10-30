using Microsoft.Extensions.Configuration;

using Ordering.API;

namespace Ordering.FunctionalTests
{
    public class OrderingTestsStartup : Startup
    {
        public OrderingTestsStartup(IConfiguration env)
            : base(env)
        {

        }
    }
}
