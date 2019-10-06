namespace Ordering.Domain.SeedWork
{
    /// <summary>
    /// The base generic interface of a repository which accepts an aggregate root as T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
