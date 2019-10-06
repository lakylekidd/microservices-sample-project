using System.Threading.Tasks;

using Ordering.Domain.SeedWork;

namespace Ordering.Domain.AggregatesModel.OrderAggregate
{
    /// <summary>
    /// This is just the RepositoryContracts or Interface defined at the Domain Layer as a requirement for the Order Aggregate
    /// </summary>
    public interface IOrderRepository : IRepository<Order>
    {
        Order Add(Order order);

        void Update(Order order);

        Task<Order> GetAsync(int orderId);
    }
}
