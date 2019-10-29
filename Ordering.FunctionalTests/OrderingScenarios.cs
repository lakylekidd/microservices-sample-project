using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Public.Services.DTOs;
using Xunit;

namespace Ordering.FunctionalTests
{
    public class OrderingScenarios : OrderingScenarioBase
    {

        public OrderingScenarios()
        {
                
        }

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

        [Trait("Order API", "Functional Tests for the entire Microservice")]
        [Fact(DisplayName = "Cancels an order which was not already created - bad request response")]
        public async Task Cancel_order_no_order_created_bad_request_response()
        {
            using (var server = CreateServer())
            {
                var content = new StringContent(BuildOrder(), Encoding.UTF8, "application/json");
                var response = await server.CreateIdempotentClient()
                    .PutAsync(Endpoints.Put.CancelOrder, content);

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        /*
         * PRIVATE FUNCTIONS
         */

        // Build a new order DTO object
        private static string BuildOrder()
        {
            var order = new OrderDTO()
            {
                OrderNumber = "-1"
            };
            return JsonConvert.SerializeObject(order);
        }
    }
}
