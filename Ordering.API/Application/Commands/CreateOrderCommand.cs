using System;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

using Ordering.API.Application.Models;

namespace Ordering.API.Application.Commands
{
    // DDD and CQRS patterns comment: Note that it is recommended to implement immutable Commands
    // In this case, its immutability is achieved by having all the setters as private
    // plus only being able to update the data just once, when creating the object through its constructor.
    // References on Immutable Commands:  
    // http://cqrs.nu/Faq
    // https://docs.spine3.org/motivation/immutability.html 
    // http://blog.gauffin.org/2012/06/griffin-container-introducing-command-support/
    // https://msdn.microsoft.com/en-us/library/bb383979.aspx

    /// <summary>
    /// The command that creates a new order
    /// </summary>
    [DataContract]
    public class CreateOrderCommand
        : IRequest<bool>
    {
        /// <summary>
        /// A DTO object that refers to an order item
        /// </summary>
        public class OrderItemDTO
        {
            /// <summary>
            /// The product id
            /// </summary>
            public int ProductId { get; set; }

            /// <summary>
            /// The product name
            /// </summary>
            public string ProductName { get; set; }

            /// <summary>
            /// The unit price
            /// </summary>
            public decimal UnitPrice { get; set; }

            /// <summary>
            /// A discount if available
            /// </summary>
            public decimal Discount { get; set; }

            /// <summary>
            /// The number of units
            /// </summary>
            public int Units { get; set; }

            /// <summary>
            /// The picture of the product
            /// </summary>
            public string PictureUrl { get; set; }
        }

        // Private readonly list of order item DTOs
        [DataMember]
        private readonly List<OrderItemDTO> _orderItems;

        /// <summary>
        /// The buyer's user ID
        /// </summary>
        [DataMember]
        public string UserId { get; private set; }

        /// <summary>
        /// The buyer's username
        /// </summary>
        [DataMember]
        public string UserName { get; private set; }

        /// <summary>
        /// The city of the buyer
        /// </summary>
        [DataMember]
        public string City { get; private set; }

        /// <summary>
        /// The street
        /// </summary>
        [DataMember]
        public string Street { get; private set; }

        /// <summary>
        /// The state 
        /// </summary>
        [DataMember]
        public string State { get; private set; }

        /// <summary>
        /// The country
        /// </summary>
        [DataMember]
        public string Country { get; private set; }

        /// <summary>
        /// The zip code
        /// </summary>
        [DataMember]
        public string ZipCode { get; private set; }

        /// <summary>
        /// The card number associated with this order
        /// </summary>
        [DataMember]
        public string CardNumber { get; private set; }

        /// <summary>
        /// The card holder's name
        /// </summary>
        [DataMember]
        public string CardHolderName { get; private set; }

        /// <summary>
        /// The expiration date of the card
        /// </summary>
        [DataMember]
        public DateTime CardExpiration { get; private set; }

        /// <summary>
        /// The security number of the card
        /// </summary>
        [DataMember]
        public string CardSecurityNumber { get; private set; }

        /// <summary>
        /// The card type id
        /// </summary>
        [DataMember]
        public int CardTypeId { get; private set; }

        /// <summary>
        /// The list of ordered items
        /// </summary>
        [DataMember]
        public IEnumerable<OrderItemDTO> OrderItems => _orderItems;

        // The default constructor
        public CreateOrderCommand()
        {
            _orderItems = new List<OrderItemDTO>();
        }

        // The constructor
        public CreateOrderCommand(List<BasketItem> basketItems, string userId, string userName, string city, string street, string state, string country, string zipcode,
            string cardNumber, string cardHolderName, DateTime cardExpiration,
            string cardSecurityNumber, int cardTypeId) : this()
        {
            _orderItems = basketItems.ToOrderItemsDTO().ToList();
            UserId = userId;
            UserName = userName;
            City = city;
            Street = street;
            State = state;
            Country = country;
            ZipCode = zipcode;
            CardNumber = cardNumber;
            CardHolderName = cardHolderName;
            CardExpiration = cardExpiration;
            CardSecurityNumber = cardSecurityNumber;
            CardTypeId = cardTypeId;
            CardExpiration = cardExpiration;
        }
    }
}
