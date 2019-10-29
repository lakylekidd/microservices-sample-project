using System;
using System.Collections.Generic;
using Ordering.API.Application.Commands;

namespace Ordering.API.Application.Models
{
    /// <summary>
    /// A basket item that refers to a unique product properties in the basket
    /// </summary>
    public class BasketItem
    {
        /// <summary>
        /// The id of the basket item
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// The product id this basket item refers to
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// The product name
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// The unit price of the item
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Any old unit prices of the item
        /// </summary>
        public decimal OldUnitPrice { get; set; }

        /// <summary>
        /// The quantity of the item
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// A picture of the product
        /// </summary>
        public string PictureUrl { get; set; }
    }

    /// <summary>
    /// Basket item extension methods
    /// </summary>
    public static class BasketItemExtensions
    {
        /// <summary>
        /// Converts an enumerable list of basket items into an enumerable list of order item DTOs
        /// </summary>
        /// <param name="basketItems"></param>
        /// <returns></returns>
        public static IEnumerable<CreateOrderCommand.OrderItemDTO> ToOrderItemsDTO(this IEnumerable<BasketItem> basketItems)
        {
            foreach (var item in basketItems)
            {
                yield return item.ToOrderItemDTO();
            }
        }

        /// <summary>
        /// Converts a basket item into an order item DTO
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static CreateOrderCommand.OrderItemDTO ToOrderItemDTO(this BasketItem item)
        {
            return new CreateOrderCommand.OrderItemDTO()
            {
                ProductId = Int32.TryParse(item.ProductId, out int id) ? id : -1,
                ProductName = item.ProductName,
                PictureUrl = item.PictureUrl,
                UnitPrice = item.UnitPrice,
                Units = item.Quantity
            };
        }
    }
}
