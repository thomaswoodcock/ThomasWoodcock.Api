using System.Collections.Generic;
using System.Threading.Tasks;

using ThomasWoodcock.Service.Domain.SeedWork;

namespace ThomasWoodcock.Service.Application.Common.DomainEvents
{
    /// <summary>
    ///     Allows a class to act as a publisher of domain events.
    /// </summary>
    public interface IDomainEventPublisher
    {
        /// <summary>
        ///     Publishes one or more domain events.
        /// </summary>
        /// <param name="domainEvents">
        ///     The domain events to publish.
        /// </param>
        Task PublishAsync(IEnumerable<IDomainEvent> domainEvents);
    }
}
