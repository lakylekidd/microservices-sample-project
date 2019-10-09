using System;
using System.Linq;
using System.Collections.Generic;

using Ordering.Domain.SeedWork;
using Ordering.Domain.Events;

namespace Ordering.Domain.AggregatesModel.BuyerAggregate
{
    public class Buyer : Entity, IAggregateRoot
    {
        private List<PaymentMethod> _paymentMethods;

        /// <summary>
        /// The buyer's identity id
        /// </summary>
        public string IdentityGuid { get; private set; }

        /// <summary>
        /// The buyer's name
        /// </summary>
        public string Name { get; private set; }
        
        /// <summary>
        /// A list of the buyer's payment methods
        /// </summary>
        public IEnumerable<PaymentMethod> PaymentMethods => _paymentMethods.AsReadOnly();

        protected Buyer()
        {
            _paymentMethods = new List<PaymentMethod>();
        }

        public Buyer(string identity, string name) : this()
        {
            IdentityGuid = !string.IsNullOrWhiteSpace(identity) ? identity : throw new ArgumentNullException(nameof(identity));
            Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullException(nameof(name));
        }

        /// <summary>
        /// Will verify or add a new payment method to the buyer
        /// </summary>
        /// <param name="cardTypeId"></param>
        /// <param name="alias"></param>
        /// <param name="cardNumber"></param>
        /// <param name="securityNumber"></param>
        /// <param name="cardHolderName"></param>
        /// <param name="expiration"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public PaymentMethod VerifyOrAddPaymentMethod(
            int cardTypeId, string alias, string cardNumber,
            string securityNumber, string cardHolderName, DateTime expiration, int orderId)
        {
            var existingPayment = _paymentMethods
                .SingleOrDefault(p => p.IsEqualTo(cardTypeId, cardNumber, expiration));

            if (existingPayment != null)
            {
                AddDomainEvent(new BuyerAndPaymentMethodVerifiedDomainEvent(this, existingPayment, orderId));

                return existingPayment;
            }

            var payment = new PaymentMethod(cardTypeId, alias, cardNumber, securityNumber, cardHolderName, expiration);

            _paymentMethods.Add(payment);

            AddDomainEvent(new BuyerAndPaymentMethodVerifiedDomainEvent(this, payment, orderId));

            return payment;
        }
    }
}