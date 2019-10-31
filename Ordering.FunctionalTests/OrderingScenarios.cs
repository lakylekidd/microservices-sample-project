using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microservices.Library.EventBus.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Ordering.API.Application.IntegrationEvents.Events;
using Ordering.API.Application.Models;
using Public.Services.DTOs;
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

        [Trait("Order API", "Functional Tests for the entire Microservice")]
        [Fact(DisplayName = "Cancels an order which was not already created - bad request response")]
        public async Task Cancel_order_no_order_created_bad_request_response()
        {
            using (var server = CreateServer())
            {
                var content = new StringContent(BuildOrder(), Encoding.UTF8, "application/json");
                try
                {
                    var response = await server.CreateIdempotentClient()
                        .PutAsync(Endpoints.Put.CancelOrder, content);
                    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        [Trait("Order API", "Functional Tests for the entire Microservice")]
        [Fact(DisplayName = "Testing")]
        public async Task Testing()
        {
            using (var server = CreateServer())
            {
                var eventBus = server.Host.Services.GetService<IEventBus>();

                var requestId = Guid.NewGuid();
                var items = new List<BasketItem>
                {
                    new BasketItem() { Id = "1", OldUnitPrice = 10.0m, PictureUrl = "", ProductId = "1", ProductName = "Test Product 1", Quantity = 2, UnitPrice = 11.0m},
                    new BasketItem() { Id = "2", OldUnitPrice = 10.0m, PictureUrl = "", ProductId = "3", ProductName = "Test Product 3", Quantity = 1, UnitPrice = 11.0m},
                };
                var basket = new CustomerBasket("1") { Items = items };
                eventBus.Publish(new UserCheckoutAcceptedIntegrationEvent("1", "billyvlachos", "Amsterdam", "Schaarsbergenstraat", "Noord-Holland", "Netherlands", "1107JV",
                    "375301410681000", "V. Vlachos", DateTime.Parse("01/12/22"), "8925", 2, "Billy Vlachos", requestId, basket));
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
