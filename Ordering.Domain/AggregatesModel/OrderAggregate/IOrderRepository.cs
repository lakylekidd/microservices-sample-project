using System.Threading.Tasks;

using Ordering.Domain.SeedWork;

namespace Ordering.Domain.AggregatesModel.OrderAggregate
{
    /// <summary>
    /// This is just the RepositoryContracts or Interface defined at the Domain Layer as a requirement for the Order Aggregate
    /// </summary>
    public interface IOrderRepository : IRepository<Order>
    {
        /// <summary>
        /// Adds an order into the repository
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        Order Add(Order order);

        /// <summary>
        /// Updates the order 
        /// </summary>
        /// <param name="order"></param>
        void Update(Order order);

        /// <summary>
        /// Returns an order based on provided order id asynchronously
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Task<Order> GetAsync(int orderId);
    }
}
