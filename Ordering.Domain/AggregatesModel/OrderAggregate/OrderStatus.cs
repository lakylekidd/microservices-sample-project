namespace Ordering.Domain.AggregatesModel.OrderAggregate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Ordering.Domain.Exceptions;
    using Ordering.Domain.SeedWork;

    /// <summary>
    /// A class that represent all the available Order Statuses as an <see cref="Enumeration"/> of an Order Aggregate in the Domain.
    /// </summary>
    public class OrderStatus : Enumeration
    {
        public static OrderStatus Submitted = new OrderStatus(1, nameof(Submitted).ToLowerInvariant());
        public static OrderStatus AwaitingValidation = new OrderStatus(2, nameof(AwaitingValidation).ToLowerInvariant());
        public static OrderStatus StockConfirmed = new OrderStatus(3, nameof(StockConfirmed).ToLowerInvariant());
        public static OrderStatus Paid = new OrderStatus(4, nameof(Paid).ToLowerInvariant());
        public static OrderStatus Shipped = new OrderStatus(5, nameof(Shipped).ToLowerInvariant());
        public static OrderStatus Cancelled = new OrderStatus(6, nameof(Cancelled).ToLowerInvariant());

        /// <summary>
        /// Returns a new instance of an order status enumeration
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public OrderStatus(int id, string name)
            : base(id, name)
        { }

        /// <summary>
        /// Returns a list of order statuses available
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<OrderStatus> List() =>
            new[] { Submitted, AwaitingValidation, StockConfirmed, Paid, Shipped, Cancelled };

        /// <summary>
        /// Returns an order status based on provided name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static OrderStatus FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new OrderingDomainException($"Possible values for OrderStatus: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        /// <summary>
        /// Returns an order status based on provided ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static OrderStatus From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new OrderingDomainException($"Possible values for OrderStatus: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
