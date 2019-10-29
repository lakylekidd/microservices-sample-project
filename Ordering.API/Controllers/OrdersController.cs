using System;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microservices.Library.EventBus.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ordering.API.Application.Commands;

namespace Ordering.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IMediator mediator, ILogger<OrdersController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        
        [HttpPut]
        [Route("cancel")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CancelOrderAsync([FromBody]CancelOrderCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            // Declare a variable tha twill hold the command result
            var commandResult = false;

            // Check if the request id is provided
            if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
            {
                // Convert the command into an identified command
                var requestCancelOrder = new IdentifiedCommand<CancelOrderCommand, bool>(command, guid);

                // Log sending the identified command
                _logger.LogInformation(
                    "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestCancelOrder.GetGenericTypeName(),
                    nameof(requestCancelOrder.Command.OrderNumber),
                    requestCancelOrder.Command.OrderNumber,
                    requestCancelOrder);

                // Await for the command result
                commandResult = await _mediator.Send(requestCancelOrder);
            }

            // Check if the command has failed
            if (!commandResult)
            {
                return BadRequest();
            }

            // Request successfully completed
            return Ok();
        }
    }
}
