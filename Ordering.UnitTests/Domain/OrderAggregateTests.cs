using System;
using Xunit;

using Ordering.UnitTests.Builders;
using Ordering.Domain.AggregatesModel.OrderAggregate;
using Ordering.Domain.Exceptions;
using Ordering.Domain.Events;

namespace Ordering.UnitTests.Domain
{
    public class OrderAggregateTests
    {
        [Trait("Order Item", "Unit Tests for Order Item")]
        [Fact(DisplayName ="Creates an order item successfully")]
        public void Create_order_item_success()
        {
            // Arrange    
            var productId = 1;
            var productName = "FakeProductName";
            var unitPrice = 12;
            var discount = 15;
            var pictureUrl = "FakeUrl";
            var units = 5;

            // Act 
            var fakeOrderItem = new OrderItem(productId, productName, unitPrice, discount, pictureUrl, units);
            // Assert
            Assert.NotNull(fakeOrderItem);
        }

        [Trait("Order Item", "Unit Tests for Order Item")]
        [Fact(DisplayName = "Will throw error for invalid number of units")]
        public void Invalid_number_of_units()
        {
            // Arrange    
            var productId = 1;
            var productName = "FakeProductName";
            var unitPrice = 12;
            var discount = 15;
            var pictureUrl = "FakeUrl";
            var units = -1;

            // Act - Assert
            Assert.Throws<OrderingDomainException>(() => new OrderItem(productId, productName, unitPrice, discount, pictureUrl, units));
        }

        [Trait("Order Item", "Unit Tests for Order Item")]
        [Fact(DisplayName = "Will throw error because discount greater than price")]
        public void Invalid_total_of_order_item_lower_than_discount_applied()
        {
            // Arrange    
            var productId = 1;
            var productName = "FakeProductName";
            var unitPrice = 12;
            var discount = 15;
            var pictureUrl = "FakeUrl";
            var units = 1;

            // Act - Assert
            Assert.Throws<OrderingDomainException>(() => new OrderItem(productId, productName, unitPrice, discount, pictureUrl, units));
        }

        [Trait("Order Item", "Unit Tests for Order Item")]
        [Fact(DisplayName = "Will throw error because discount set is less than 0")]
        public void Invalid_discount_setting()
        {
            // Arrange    
            var productId = 1;
            var productName = "FakeProductName";
            var unitPrice = 12;
            var discount = 15;
            var pictureUrl = "FakeUrl";
            var units = 5;

            // Act 
            var fakeOrderItem = new OrderItem(productId, productName, unitPrice, discount, pictureUrl, units);

            // Assert
            Assert.Throws<OrderingDomainException>(() => fakeOrderItem.SetNewDiscount(-1));
        }

        [Trait("Order Item", "Unit Tests for Order Item")]
        [Fact(DisplayName = "Will throw error because unit set is less than 0")]
        public void Invalid_units_setting()
        {
            // Arrange    
            var productId = 1;
            var productName = "FakeProductName";
            var unitPrice = 12;
            var discount = 15;
            var pictureUrl = "FakeUrl";
            var units = 5;

            // Act 
            var fakeOrderItem = new OrderItem(productId, productName, unitPrice, discount, pictureUrl, units);

            // Assert
            Assert.Throws<OrderingDomainException>(() => fakeOrderItem.AddUnits(-1));
        }

        [Trait("Order Item", "Unit Tests for Order Item")]
        [Fact(DisplayName = "Adding the same item twice will result in combining total")]        
        public void On_repeated_item_combine_and_sum_totals()
        {
            // Arrange - Act
            var address = new AddressBuilder().Build();
            var order = new OrderBuilder(address)
                .AddOne(1, "cup", 10.0m, 0, string.Empty)
                .AddOne(1, "cup", 10.0m, 0, string.Empty)
                .Build();
            // Assert
            Assert.Equal(20.0m, order.GetTotal());
        }

        [Trait("Order Item", "Unit Tests for Order Item")]
        [Fact(DisplayName = "Adding a new order raises a new event")]
        public void Add_new_Order_raises_new_event()
        {
            // Arrange
            var street = "fakeStreet";
            var city = "FakeCity";
            var state = "fakeState";
            var country = "fakeCountry";
            var zipcode = "FakeZipCode";
            var cardTypeId = 5;
            var cardNumber = "12";
            var cardSecurityNumber = "123";
            var cardHolderName = "FakeName";
            var cardExpiration = DateTime.Now.AddYears(1);
            var expectedResult = 1;

            // Act 
            var fakeOrder = new Order("1", "fakeName", new Address(street, city, state, country, zipcode), cardTypeId, cardNumber, cardSecurityNumber, cardHolderName, cardExpiration);

            // Assert
            Assert.Equal(fakeOrder.DomainEvents.Count, expectedResult);
        }

        [Trait("Order Item", "Unit Tests for Order Item")]
        [Fact(DisplayName = "Adding order event explicitly raises a new event")]
        public void Add_event_Order_explicitly_raises_new_event()
        {
            //Arrange   
            var street = "fakeStreet";
            var city = "FakeCity";
            var state = "fakeState";
            var country = "fakeCountry";
            var zipcode = "FakeZipCode";
            var cardTypeId = 5;
            var cardNumber = "12";
            var cardSecurityNumber = "123";
            var cardHolderName = "FakeName";
            var cardExpiration = DateTime.Now.AddYears(1);
            var expectedResult = 2;

            //Act 
            var fakeOrder = new Order("1", "fakeName", new Address(street, city, state, country, zipcode), cardTypeId, cardNumber, cardSecurityNumber, cardHolderName, cardExpiration);
            fakeOrder.AddDomainEvent(new OrderStartedDomainEvent(fakeOrder, "fakeName", "1", cardTypeId, cardNumber, cardSecurityNumber, cardHolderName, cardExpiration));
            //Assert
            Assert.Equal(fakeOrder.DomainEvents.Count, expectedResult);
        }

        [Trait("Order Item", "Unit Tests for Order Item")]
        [Fact(DisplayName = "Removing order event explicitly removes the event")]
        public void Remove_event_Order_explicitly()
        {
            // Arrange    
            var street = "fakeStreet";
            var city = "FakeCity";
            var state = "fakeState";
            var country = "fakeCountry";
            var zipcode = "FakeZipCode";
            var cardTypeId = 5;
            var cardNumber = "12";
            var cardSecurityNumber = "123";
            var cardHolderName = "FakeName";
            var cardExpiration = DateTime.Now.AddYears(1);
            var fakeOrder = new Order("1", "fakeName", new Address(street, city, state, country, zipcode), cardTypeId, cardNumber, cardSecurityNumber, cardHolderName, cardExpiration);
            var @fakeEvent = new OrderStartedDomainEvent(fakeOrder, "1", "fakeName", cardTypeId, cardNumber, cardSecurityNumber, cardHolderName, cardExpiration);
            var expectedResult = 1;

            // Act         
            fakeOrder.AddDomainEvent(@fakeEvent);
            fakeOrder.RemoveDomainEvent(@fakeEvent);
            // Assert
            Assert.Equal(fakeOrder.DomainEvents.Count, expectedResult);
        }
    }
}
