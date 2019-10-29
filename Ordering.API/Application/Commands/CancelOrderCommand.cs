using MediatR;
using System.Runtime.Serialization;

namespace Ordering.API.Application.Commands
{
    /// <summary>
    /// The cancel order command
    /// </summary>
    public class CancelOrderCommand : IRequest<bool>
    {
        /// <summary>
        /// The order number to cancel
        /// </summary>
        [DataMember]
        public int OrderNumber { get; private set; }

        // The command constructor
        public CancelOrderCommand(int orderNumber)
        {
            OrderNumber = orderNumber;
        }
    }
}
