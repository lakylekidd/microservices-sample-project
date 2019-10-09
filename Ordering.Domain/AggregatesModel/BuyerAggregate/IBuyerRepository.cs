using System.Threading.Tasks;

namespace Ordering.Domain.AggregatesModel.BuyerAggregate
{
    /// <summary>
    /// This is just the RepositoryContracts or Interface defined at the Domain Layer as requisite for the Buyer Aggregate
    /// </summary>
    public interface IBuyerRepository
    {
        /// <summary>
        /// Adds a buyer into the repository
        /// </summary>
        /// <param name="buyer"></param>
        /// <returns></returns>
        Buyer Add(Buyer buyer);

        /// <summary>
        /// Updates the order
        /// </summary>
        /// <param name="buyer"></param>
        /// <returns></returns>
        Buyer Update(Buyer buyer);

        /// <summary>
        /// Returns a buyer entity based on provided buyer identity id
        /// </summary>
        /// <param name="BuyerIdentityGuid"></param>
        /// <returns></returns>
        Task<Buyer> FindAsync(string BuyerIdentityGuid);

        /// <summary>
        /// Returns a buyer entity based on provided buyer id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Buyer> FindByIdAsync(string id);
    }
}
