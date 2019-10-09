using System;
using Xunit;

using Ordering.Domain.AggregatesModel.BuyerAggregate;
using Ordering.Domain.Exceptions;
using System.Linq;

namespace Ordering.UnitTests.Domain
{
    public class BuyerAggregateTests
    {
        [Trait("Buyer", "Unit Tests for Buyer Aggregate")]
        [Fact(DisplayName = "Creates a buyer successfully")]
        public void Create_buyer_item_success()
        {
            // Arrange    
            var identity = new Guid().ToString();
            var name = "fakeUser";

            // Act 
            var fakeBuyerItem = new Buyer(identity, name);

            // Assert
            Assert.NotNull(fakeBuyerItem);
        }

        [Trait("Buyer", "Unit Tests for Buyer Aggregate")]
        [Fact(DisplayName = "Creates a buyer fails without identity id")]
        public void Create_buyer_item_fail()
        {
            // Arrange    
            var identity = string.Empty;
            var name = "fakeUser";

            // Act - Assert
            Assert.Throws<ArgumentNullException>(() => new Buyer(identity, name));
        }

        [Trait("Buyer", "Unit Tests for Buyer Aggregate")]
        [Fact(DisplayName = "Adds a new payment method to buyer successfully")]
        public void Add_payment_success()
        {
            // Arrange    
            var cardTypeId = 1;
            var alias = "fakeAlias";
            var cardNumber = "124";
            var securityNumber = "1234";
            var cardHolderName = "FakeHolderNAme";
            var expiration = DateTime.Now.AddYears(1);
            var orderId = 1;
            var name = "fakeUser";
            var identity = new Guid().ToString();
            var fakeBuyerItem = new Buyer(identity, name);

            // Act
            var result = fakeBuyerItem.VerifyOrAddPaymentMethod(cardTypeId, alias, cardNumber, securityNumber, cardHolderName, expiration, orderId);

            // Assert
            Assert.NotNull(result);
            Assert.Single(fakeBuyerItem.PaymentMethods);
        }

        [Trait("Buyer", "Unit Tests for Buyer Aggregate")]
        [Fact(DisplayName = "Creates a new payment method successfully")]
        public void Create_payment_method_success()
        {
            // Arrange    
            var cardTypeId = 1;
            var alias = "fakeAlias";
            var cardNumber = "124";
            var securityNumber = "1234";
            var cardHolderName = "FakeHolderNAme";
            var expiration = DateTime.Now.AddYears(1);

            // Act
            var result = new PaymentMethod(cardTypeId, alias, cardNumber, securityNumber, cardHolderName, expiration);

            // Assert
            Assert.NotNull(result);
        }

        [Trait("Buyer", "Unit Tests for Buyer Aggregate")]
        [Fact(DisplayName = "Will fail to create payment method with expired card")]
        public void Create_payment_method_expiration_fail()
        {
            // Arrange    
            var cardTypeId = 1;
            var alias = "fakeAlias";
            var cardNumber = "124";
            var securityNumber = "1234";
            var cardHolderName = "FakeHolderNAme";
            var expiration = DateTime.Now.AddYears(-1);

            // Act - Assert
            Assert.Throws<OrderingDomainException>(() => new PaymentMethod(cardTypeId, alias, cardNumber, securityNumber, cardHolderName, expiration));
        }

        [Trait("Buyer", "Unit Tests for Buyer Aggregate")]
        [Fact(DisplayName = "Payment method will succeed comparison of equal card details")]
        public void Payment_method_isEqualTo()
        {
            // Arrange    
            var cardTypeId = 1;
            var alias = "fakeAlias";
            var cardNumber = "124";
            var securityNumber = "1234";
            var cardHolderName = "FakeHolderNAme";
            var expiration = DateTime.Now.AddYears(1);

            // Act
            var fakePaymentMethod = new PaymentMethod(cardTypeId, alias, cardNumber, securityNumber, cardHolderName, expiration);
            var result = fakePaymentMethod.IsEqualTo(cardTypeId, cardNumber, expiration);

            // Assert
            Assert.True(result);
        }

        [Trait("Buyer", "Unit Tests for Buyer Aggregate")]
        [Fact(DisplayName = "Adding a new payment method raises a new event")]
        public void Add_new_PaymentMethod_raises_new_event()
        {
            // Arrange    
            var alias = "fakeAlias";
            var orderId = 1;
            var cardTypeId = 5;
            var cardNumber = "12";
            var cardSecurityNumber = "123";
            var cardHolderName = "FakeName";
            var cardExpiration = DateTime.Now.AddYears(1);
            var expectedResult = 1;
            var name = "fakeUser";

            // Act 
            var fakeBuyer = new Buyer(Guid.NewGuid().ToString(), name);
            fakeBuyer.VerifyOrAddPaymentMethod(cardTypeId, alias, cardNumber, cardSecurityNumber, cardHolderName, cardExpiration, orderId);

            // Assert
            Assert.Equal(fakeBuyer.DomainEvents.Count, expectedResult);
        }
    }
}
