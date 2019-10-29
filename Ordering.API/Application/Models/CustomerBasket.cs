using System.Collections.Generic;

namespace Ordering.API.Application.Models
{
    /// <summary>
    /// The customer basket that holds a list of <see cref="BasketItem"/>
    /// </summary>
    public class CustomerBasket
    {
        /// <summary>
        /// The buyer id
        /// </summary>
        public string BuyerId { get; set; }

        /// <summary>
        /// A list of basket items
        /// </summary>
        public List<BasketItem> Items { get; set; }

        // The constructor
        public CustomerBasket(string customerId)
        {
            BuyerId = customerId;
            Items = new List<BasketItem>();
        }
    }
}
