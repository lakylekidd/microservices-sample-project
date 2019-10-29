using System.Threading.Tasks;
using Xunit;

namespace Ordering.FunctionalTests
{
    public class OrderingScenarios : OrderingScenarioBase
    {
        [Trait("Order API", "Functional Tests for the entire Microservice")]
        [Fact(DisplayName = "Gets the values and responds ok 200")]
        public async Task Get_values_and_response_ok_status_code()
        {
            using (var server = CreateServer())
            {
                var response = await server
                    .CreateClient()
                    .GetAsync("api/values");

                response.EnsureSuccessStatusCode();
            }
        }
    }
}
