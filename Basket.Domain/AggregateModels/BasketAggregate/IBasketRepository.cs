using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Domain.AggregateModels.BasketAggregate
{
    public interface IBasketRepository
    {
        /// <summary>
        /// Returns a single basket based on customer id
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <returns></returns>
        Task<CustomerBasket> GetBasketAsync(string customerId);

        /// <summary>
        /// Updates a basket
        /// </summary>
        /// <param name="basket">The basket</param>
        /// <returns></returns>
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);

        /// <summary>
        /// Deletes a basket
        /// </summary>
        /// <param name="id">The id of the basket</param>
        /// <returns></returns>
        Task<bool> DeleteBasketAsync(string id);

        /// <summary>
        /// Returns a list of users
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetUsers();
    }
}
