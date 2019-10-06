using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Domain.SeedWork
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Save all changes made to the repository asynchronously
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Save all entities asynchronously
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
