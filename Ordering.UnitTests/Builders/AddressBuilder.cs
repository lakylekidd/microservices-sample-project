using Ordering.Domain.AggregatesModel.OrderAggregate;

namespace Ordering.UnitTests.Builders
{
    /// <summary>
    /// Address builder
    /// </summary>
    public class AddressBuilder
    {
        public Address Build()
        {
            return new Address("street", "city", "state", "country", "zipcode");
        }
    }
}
