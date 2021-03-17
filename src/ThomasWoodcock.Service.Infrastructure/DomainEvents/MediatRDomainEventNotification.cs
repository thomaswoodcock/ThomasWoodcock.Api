using MediatR;

using ThomasWoodcock.Service.Domain.SeedWork;

namespace ThomasWoodcock.Service.Infrastructure.DomainEvents
{
    /// <inheritdoc cref="INotification" />
    /// <summary>
    ///     An implementation of the <see cref="INotification" /> interface used to publish domain events using MediatR.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of domain event that will be published.
    /// </typeparam>
    /// <param name="DomainEvent">
    ///     The domain event that will be published.
    /// </param>
    internal sealed record MediatRDomainEventNotification<T>(T DomainEvent) : INotification
        where T : class, IDomainEvent;
}
