using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Domain.AggregateModels.BasketAggregate
{
    public class CustomerBasket
    {
        /// <summary>
        /// The buyer id
        /// </summary>
        public string BuyerId { get; private set; }
        
        private List<BasketItem> _items;
        /// <summary>
        /// A list of readonly basket items
        /// </summary>
        public IEnumerable<BasketItem> Items => _items.AsReadOnly();

        public CustomerBasket(string buyerId)
        {
            BuyerId = buyerId;
            _items = new List<BasketItem>();
        }
    }
}
