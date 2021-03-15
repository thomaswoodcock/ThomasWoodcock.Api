using System.Threading.Tasks;

using ThomasWoodcock.Service.Domain.SeedWork;

namespace ThomasWoodcock.Service.Application.Common.DomainEvents
{
    /// <summary>
    ///     Allows a class to act as a handler of <see cref="IDomainEvent" /> objects of type <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of domain event that will be handled.
    /// </typeparam>
    internal interface IDomainEventHandler<in T>
        where T : class, IDomainEvent
    {
        /// <summary>
        ///     Handles the given domain event.
        /// </summary>
        /// <param name="domainEvent">
        ///     The domain event to handle.
        /// </param>
        Task HandleAsync(T domainEvent);
    }
}
