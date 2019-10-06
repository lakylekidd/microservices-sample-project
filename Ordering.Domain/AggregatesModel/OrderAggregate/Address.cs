using Ordering.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace Ordering.Domain.AggregatesModel.OrderAggregate
{
    /// <summary>
    /// The address value object
    /// </summary>
    public class Address : ValueObject
    {
        /// <summary>
        /// The street
        /// </summary>
        public String Street { get; private set; }

        /// <summary>
        /// The city
        /// </summary>
        public String City { get; private set; }

        /// <summary>
        /// The state
        /// </summary>
        public String State { get; private set; }

        /// <summary>
        /// The country
        /// </summary>
        public String Country { get; private set; }

        /// <summary>
        /// The zip code
        /// </summary>
        public String ZipCode { get; private set; }

        private Address() { }

        /// <summary>
        /// Creates a new instance of an Address value object
        /// </summary>
        /// <param name="street"></param>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="country"></param>
        /// <param name="zipcode"></param>
        public Address(string street, string city, string state, string country, string zipcode)
        {
            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipcode;
        }

        /// <summary>
        /// Returns an enumerable of each property of the Address value object
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time
            yield return Street;
            yield return City;
            yield return State;
            yield return Country;
            yield return ZipCode;
        }
    }
}
