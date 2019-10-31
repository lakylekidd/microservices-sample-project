using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using Public.Services.DTOs;
using Xunit;

namespace Ordering.FunctionalTests
{
    public class OrderingControllerIntegrationTest : IntegrationTest
    {
        [Fact]
        public async Task CancelOrder_fake_order_number_returns_bad_request()
        {
            var content = new StringContent(BuildOrder(), Encoding.UTF8, "application/json");
            var response = await TestHttpClient.PutAsync(Endpoints.Put.CancelOrder, content);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
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
