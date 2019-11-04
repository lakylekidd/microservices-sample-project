using System;

namespace Basket.Domain.AggregateModels.BasketAggregate
{
    public class BasketCheckout
    {
        /// <summary>
        /// The delivery city
        /// </summary>
        public string City { get; private set; }

        /// <summary>
        /// The delivery street
        /// </summary>
        public string Street { get; private set; }

        /// <summary>
        /// The delivery state
        /// </summary>
        public string State { get; private set; }

        /// <summary>
        /// The delivery country
        /// </summary>
        public string Country { get; private set; }

        /// <summary>
        /// The delivery zip code
        /// </summary>
        public string ZipCode { get; private set; }

        /// <summary>
        /// The credit card to charge the order
        /// </summary>
        public string CardNumber { get; private set; }

        /// <summary>
        /// The holder name on the credit card
        /// </summary>
        public string CardHolderName { get; private set; }

        /// <summary>
        /// The expiration date of the credit card
        /// </summary>
        public DateTime CardExpiration { get; private set; }
        /// <summary>
        /// The credit card security number
        /// </summary>
        public string CardSecurityNumber { get; private set; }

        /// <summary>
        /// The card type id
        /// </summary>
        public int CardTypeId { get; private set; }

        /// <summary>
        /// The buyer name
        /// </summary>
        public string Buyer { get; private set; }

        /// <summary>
        /// The request id
        /// </summary>
        public Guid RequestId { get; private set; }

        // The main constructor
        public BasketCheckout(string city, string street, string state, string country, string zipCode,
            string cardNumber, string cardHolderName, DateTime cardExpiration, string cardSecurityNumber,
            int cardTypeId, string buyer, Guid requestId)
        {
            CardExpiration = cardExpiration;
            CardSecurityNumber = cardSecurityNumber;
            City = city;
            Street = street;
            State = state;
            Country = country;
            ZipCode = zipCode;
            CardNumber = cardNumber;
            CardHolderName = cardHolderName;
            CardTypeId = cardTypeId;
            Buyer = buyer;
            RequestId = requestId;
        }
    }
}
