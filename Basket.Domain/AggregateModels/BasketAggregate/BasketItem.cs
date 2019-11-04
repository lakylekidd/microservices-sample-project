using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Basket.Domain.AggregateModels.BasketAggregate
{
    public class BasketItem
    {
        /// <summary>
        /// The id of the basket item
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The product id
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// The product name
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// The price of the product
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// The old price of the product
        /// </summary>
        public decimal OldUnitPrice { get; set; }

        /// <summary>
        /// The quantity of the product
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// A picture url of the product
        /// </summary>
        public string PictureUrl { get; set; }

        /// <summary>
        /// Validates if the product quantity is valid
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (Quantity < 1)
            {
                results.Add(new ValidationResult("Invalid number of units", new[] { "Quantity" }));
            }

            return results;
        }
    }
}
