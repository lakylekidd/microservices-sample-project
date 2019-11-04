using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Basket.API.Application.IntegrationEvents.Events;
using Basket.API.Services;
using Basket.Domain.AggregateModels.BasketAggregate;
using Microservices.Library.EventBus.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Basket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly ILogger<BasketController> _logger;
        private readonly IBasketRepository _basketRepository;
        private readonly IIdentityService _identityService;
        private readonly IEventBus _eventBus;

        public BasketController(ILogger<BasketController> logger, IBasketRepository basketRepository,
            IIdentityService identityService, IEventBus eventBus)
        {
            _logger = logger;
            _basketRepository = basketRepository;
            _identityService = identityService;
            _eventBus = eventBus;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerBasket), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<CustomerBasket>> GetBasketByIdAsync(string id)
        {
            // Retrieve the basked using the basket id
            var basket = await _basketRepository.GetBasketAsync(id);
            // Return the result or a new basked with provided id
            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerBasket), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CustomerBasket>> UpdateBasketAsync([FromBody]CustomerBasket value)
        {
            // Update the basket and return the result
            return Ok(await _basketRepository.UpdateBasketAsync(value));
        }

        [HttpPost]
        [Route("checkout")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> CheckoutAsync([FromBody]BasketCheckout basketCheckout, [FromHeader(Name = "x-requestid")] string requestId)
        {
            // Retrieve the user id from the identity service
            var userId = _identityService.GetUserIdentity();
            // Set the request id for the basked
            basketCheckout.SetRequestId((Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty) ?
                guid : basketCheckout.RequestId);
            // Retrieve the basket
            var basket = await _basketRepository.GetBasketAsync(userId);

            // Check if basked was retrieved
            if (basket == null)
            {
                // If not then return bad request
                return BadRequest();
            }

            // Find the username of the user
            var userName = User.FindFirst(x => x.Type == "unique_name").Value;

            // Create the event based on the basket
            var eventMessage = new UserCheckoutAcceptedIntegrationEvent(userId, userName, basketCheckout.City, basketCheckout.Street,
                basketCheckout.State, basketCheckout.Country, basketCheckout.ZipCode, basketCheckout.CardNumber, basketCheckout.CardHolderName,
                basketCheckout.CardExpiration, basketCheckout.CardSecurityNumber, basketCheckout.CardTypeId, basketCheckout.Buyer, basketCheckout.RequestId, basket);

            // Once basket is checkout, sends an integration event to
            // ordering.api to convert basket to order and proceeds with
            // order creation process
            try
            {
                // Log publishing the event and publish it
                _logger.LogInformation("----- Publishing integration event: {IntegrationEventId} from {AppName} - ({@IntegrationEvent})", eventMessage.Id, Program.AppName, eventMessage);
                _eventBus.Publish(eventMessage);
            }
            catch (Exception ex)
            {
                // Log any errors and throw them
                _logger.LogError(ex, "ERROR Publishing integration event: {IntegrationEventId} from {AppName}", eventMessage.Id, Program.AppName);
                throw;
            }

            // Return that request has been accepted
            return Accepted();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task DeleteBasketByIdAsync(string id)
        {
            // Delete the basked with provided id
            await _basketRepository.DeleteBasketAsync(id);
        }
    }
}
