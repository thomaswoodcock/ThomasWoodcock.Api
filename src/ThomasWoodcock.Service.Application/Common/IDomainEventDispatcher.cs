using System.Collections.Generic;
using System.Threading.Tasks;

using ThomasWoodcock.Service.Domain.SeedWork;

namespace ThomasWoodcock.Service.Application.Common
{
    /// <summary>
    ///     Allows a class to act as a dispatcher of domain events.
    /// </summary>
    public interface IDomainEventDispatcher
    {
        /// <summary>
        ///     Dispatches one or more domain events.
        /// </summary>
        /// <param name="domainEvents">
        ///     The domain events to dispatch.
        /// </param>
        Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents);
    }
}
