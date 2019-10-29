using MediatR;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Ordering.API.Application.Commands;
using Ordering.Domain.AggregatesModel.OrderAggregate;
using App.Services.Ordering.Infrastructure.Idempotency;

namespace Ordering.API.Application.CommandHandlers
{
    // Regular CommandHandler
    public class CancelOrderCommandHandler 
        : IRequestHandler<CancelOrderCommand, bool>
    {
        // The order repository
        private readonly IOrderRepository _orderRepository;

        // The command handler constructor
        public CancelOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Handler which processes the command when
        /// customer executes cancel order from app
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<bool> Handle(CancelOrderCommand command, CancellationToken cancellationToken)
        {
            var orderToUpdate = await _orderRepository.GetAsync(command.OrderNumber);
            if (orderToUpdate == null)
            {
                return false;
            }

            orderToUpdate.SetCancelledStatus();
            return await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }


    // Use for Idempotency in Command process
    public class CancelOrderIdentifiedCommandHandler : IdentifiedCommandHandler<CancelOrderCommand, bool>
    {
        public CancelOrderIdentifiedCommandHandler(
            IMediator mediator,
            IRequestManager requestManager,
            ILogger<IdentifiedCommandHandler<CancelOrderCommand, bool>> logger)
            : base(mediator, requestManager, logger)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return true;                // Ignore duplicate requests for processing order.
        }
    }
}
