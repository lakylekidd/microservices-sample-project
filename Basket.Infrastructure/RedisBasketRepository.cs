using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Basket.Domain.AggregateModels.BasketAggregate;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Basket.Infrastructure
{
    public class RedisBasketRepository : IBasketRepository
    {
        private readonly ILogger _logger;
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        // The constructor
        public RedisBasketRepository(ILoggerFactory loggerFactory, ConnectionMultiplexer redis)
        {
            _logger = loggerFactory.CreateLogger<RedisBasketRepository>();
            _redis = redis;
            _database = redis.GetDatabase();
        }

        public async Task<CustomerBasket> GetBasketAsync(string customerId)
        {
            // Get the basket data based on customer id
            var data = await _database.StringGetAsync(customerId);
            // Return the data deserialized or null if not found
            return data.IsNullOrEmpty ? null : JsonConvert.DeserializeObject<CustomerBasket>(data);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            // Update the record using the buyer id of the basket.
            // if record for buyer does not exist, create it.
            var created = await _database.StringSetAsync(basket.BuyerId, JsonConvert.SerializeObject(basket));

            // Check if record was created successfully
            if (!created)
            {
                // Record creation/update failed. Report problem and return null
                _logger.LogInformation("Problem occur persisting the item.");
                return null;
            }
            // Record creation succeeded.
            _logger.LogInformation("Basket item persisted succesfully.");

            // Return updated basket
            return await GetBasketAsync(basket.BuyerId);
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }

        public IEnumerable<string> GetUsers()
        {
            // Retrieve the server and data
            var server = GetServer();
            var data = server.Keys();
            // Return only the keys of selected server
            return data?.Select(k => k.ToString());
        }

        // Get the server
        private IServer GetServer()
        {
            var endpoint = _redis.GetEndPoints();
            return _redis.GetServer(endpoint.First());
        }
    }
}
