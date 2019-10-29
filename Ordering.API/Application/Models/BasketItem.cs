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
}
