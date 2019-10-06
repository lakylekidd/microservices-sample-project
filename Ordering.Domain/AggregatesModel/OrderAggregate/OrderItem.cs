using Ordering.Domain.Exceptions;
using Ordering.Domain.SeedWork;

namespace Ordering.Domain.AggregatesModel.OrderAggregate
{
    /// <summary>
    /// A class that represents an order item entity that belongs to an Order Aggregate
    /// </summary>
    public class OrderItem : Entity
    {
        private string _productName;
        private string _pictureUrl;
        private decimal _unitPrice;
        private decimal _discount;
        private int _units;

        /// <summary>
        /// The product id
        /// </summary>
        public int ProductId { get; private set; }

        protected OrderItem() { }

        /// <summary>
        /// Returns a new instance of an order item
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="productName"></param>
        /// <param name="unitPrice"></param>
        /// <param name="discount"></param>
        /// <param name="PictureUrl"></param>
        /// <param name="units"></param>
        public OrderItem(int productId, string productName, decimal unitPrice, decimal discount, string PictureUrl, int units = 1)
        {
            if (units <= 0)
            {
                throw new OrderingDomainException("Invalid number of units");
            }

            if ((unitPrice * units) < discount)
            {
                throw new OrderingDomainException("The total of order item is lower than applied discount");
            }

            ProductId = productId;

            _productName = productName;
            _unitPrice = unitPrice;
            _discount = discount;
            _units = units;
            _pictureUrl = PictureUrl;
        }

        /// <summary>
        /// Returns the picture uri of the product
        /// </summary>
        /// <returns></returns>
        public string GetPictureUri() => _pictureUrl;

        /// <summary>
        /// Gets the current discount
        /// </summary>
        /// <returns></returns>
        public decimal GetCurrentDiscount()
        {
            return _discount;
        }

        /// <summary>
        /// Gets the number of units selected for this product
        /// </summary>
        /// <returns></returns>
        public int GetUnits()
        {
            return _units;
        }

        /// <summary>
        /// Gets the price of the product
        /// </summary>
        /// <returns></returns>
        public decimal GetUnitPrice()
        {
            return _unitPrice;
        }

        /// <summary>
        /// Gets the product name
        /// </summary>
        /// <returns></returns>
        public string GetOrderItemProductName() => _productName;

        /// <summary>
        /// Method that sets a new discount value to the unit's price
        /// </summary>
        /// <param name="discount"></param>
        public void SetNewDiscount(decimal discount)
        {
            if (discount < 0)
            {
                throw new OrderingDomainException("Discount is not valid");
            }

            _discount = discount;
        }

        /// <summary>
        /// Method that adds the provided number of units to the current order
        /// </summary>
        /// <param name="units"></param>
        public void AddUnits(int units)
        {
            if (units < 0)
            {
                throw new OrderingDomainException("Invalid units");
            }

            _units += units;
        }
    }
}
