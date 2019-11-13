using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using App.Services.Ordering.Infrastructure.Idempotency;
using Autofac.Extras.Moq;
using FluentAssertions;
using MediatR;
using Microservices.Library.EventBus.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Ordering.API.Application.CommandHandlers;
using Ordering.API.Application.Commands;
using Ordering.API.Application.IntegrationEvents;
using Ordering.API.Application.IntegrationEvents.EventHandlers;
using Ordering.API.Application.IntegrationEvents.Events;
using Ordering.API.Application.Models;
using Ordering.API.Controllers;
using Ordering.API.Infrastructure.Services;
using Ordering.Domain.AggregatesModel.OrderAggregate;
using Ordering.Infrastructure;
using Polly.Caching;
using Public.Services.DTOs;
using Xunit;

namespace Ordering.FunctionalTests
{
    public class OrderingControllerIntegrationTest : IntegrationTest
    {
        [Fact(DisplayName = "CancelOrder - Correct command is sent with correct id")]
        public async Task CancelOrder_verify_command_sent_with_correct_id()
        {
            // Init the mocl
            using (var mock = AutoMock.GetLoose())
            {
                // Define required variables
                var requestId = Guid.NewGuid().ToString();
                var command = new CancelOrderCommand(12);

                // Create mocks
                mock.Mock<ILogger<Logger<OrdersController>>>();
                var mediator = mock.Mock<IMediator>();

                // Configure mock
                mediator
                    .Setup(m => m.Send(It.IsAny<IRequest<bool>>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(true)
                    .Verifiable("Command was not sent.");
                
                // Create the controller
                var controller = mock.Create<OrdersController>();
                
                // Call the create function
                var result = await controller.CancelOrderAsync(command, requestId);
                mediator.Verify(x => x.Send(It.Is<IdentifiedCommand<CancelOrderCommand, bool>>(y => y.Command.OrderNumber == 12), It.IsAny<CancellationToken>()), Times.Once);
            }
        }

        [Fact(DisplayName = "CreateOrder - Correct command is sent with correct id)"]
        public async Task CreateOrder_create_order_sent_with_correct_data()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Define required variables
                var requestId = Guid.NewGuid().ToString();
                var items = new List<BasketItem>
                {
                    new BasketItem() { Id = "1", OldUnitPrice = 10.0m, PictureUrl = "", ProductId = "1", ProductName = "Test Product 1", Quantity = 2, UnitPrice = 11.0m},
                    new BasketItem() { Id = "2", OldUnitPrice = 10.0m, PictureUrl = "", ProductId = "3", ProductName = "Test Product 3", Quantity = 1, UnitPrice = 11.0m},
                };
                var command = new CreateOrderCommand(items, "userId", "userName", "city", "street", "state", "country", "zipCode",
                    "cardNumber", "cardHolderName", DateTime.Today.AddYears(2), "759", 1);

                // Create mock dependencies
                var mediator = mock.Mock<IMediator>();
                var repository = mock.Mock<IOrderRepository>();
                mock.Mock<IOrderingIntegrationEventService>();                
                mock.Mock<IIdentityService>();
                mock.Mock<ILogger<CreateOrderCommandHandler>>();

                // Configure mocks
                mediator
                    .Setup(m => m.Send(It.IsAny<IRequest<bool>>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(true)
                    .Verifiable("Command was not sent.");
                repository
                    .Setup(x => x.UnitOfWork.SaveEntitiesAsync(CancellationToken.None)).Returns(Task.FromResult(true));

                // Create the controller
                var controller = mock.Create<OrdersController>();

                // Call the create function
                var result = await controller.CreateOrderAsync(command, requestId);
                mediator.Verify(x => x.Send(It.Is<IdentifiedCommand<CreateOrderCommand, bool>>(y => y.Id.ToString() == requestId), It.IsAny<CancellationToken>()), Times.Once);
            }
        }

        


        [Fact]
        public async Task CancelOrder_fake_order_number_returns_bad_request()
        {
            var content = new StringContent(BuildOrder(), Encoding.UTF8, "application/json");
            var response = await TestIdempotentHttpClient.PutAsync(Endpoints.Put.CancelOrder, content);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        

        //[Fact]
        public async Task RequestManager_Test()
        {
            var requestManager = TestServer.Host.Services.GetService<IRequestManager>();
            var result = await requestManager.ExistAsync(Guid.Empty);
            result.Should().Be(false);
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

        private static string BuildRealOrder()
        {
            var items = new List<BasketItem>
            {
                new BasketItem() { Id = "1", OldUnitPrice = 10.0m, PictureUrl = "", ProductId = "1", ProductName = "Test Product 1", Quantity = 2, UnitPrice = 11.0m},
                new BasketItem() { Id = "2", OldUnitPrice = 10.0m, PictureUrl = "", ProductId = "3", ProductName = "Test Product 3", Quantity = 1, UnitPrice = 11.0m},
            };
            var basket = new CustomerBasket("1") { Items = items };
            var command = new CreateOrderCommand(items, "1", "Username0213", "Amsterdam", "Schaarsbergenstraat",
                "Noord-Holland", "Netherlands", "1107JV",
                "375301410681000", "V. Vlachos", DateTime.Parse("01/12/22"), "8925", 2);
            return JsonConvert.SerializeObject(command);
        }
    }
}
