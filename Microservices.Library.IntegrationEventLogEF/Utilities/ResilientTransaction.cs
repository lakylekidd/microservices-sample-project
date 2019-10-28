using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Microservices.Library.IntegrationEventLogEF.Utilities
{
    public class ResilientTransaction
    {
        private DbContext _context;
        private ResilientTransaction(DbContext context) =>
            _context = context ?? throw new ArgumentNullException(nameof(context));

        /// <summary>
        /// Creates a new instance of a resilient transaction
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static ResilientTransaction New (DbContext context) =>
            new ResilientTransaction(context);        

        /// <summary>
        /// Executes the transaction strategy async
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public async Task ExecuteAsync(Func<Task> action)
        {
            //Use of an EF Core resiliency strategy when using multiple DbContexts within an explicit BeginTransaction():
            //See: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency
            var strategy = _context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    await action();
                    transaction.Commit();
                }
            });
        }
    }
}
