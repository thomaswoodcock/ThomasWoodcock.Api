using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MediatR;

using ThomasWoodcock.Service.Application.Common.DomainEvents;
using ThomasWoodcock.Service.Domain.SeedWork;

namespace ThomasWoodcock.Service.Infrastructure.DomainEvents
{
    /// <inheritdoc />
    /// <summary>
    ///     An implementation of the <see cref="IDomainEventPublisher" /> interface used to publish domain events using
    ///     MediatR.
    /// </summary>
    internal sealed class MediatRDomainEventPublisher : IDomainEventPublisher
    {
        private readonly IPublisher _publisher;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MediatRDomainEventPublisher" /> class.
        /// </summary>
        /// <param name="publisher">
        ///     The publisher used to publish MediatR notifications.
        /// </param>
        public MediatRDomainEventPublisher(IPublisher publisher)
        {
            this._publisher = publisher;
        }

        /// <inheritdoc />
        public async Task PublishAsync(IEnumerable<IDomainEvent> domainEvents)
        {
            foreach (IDomainEvent domainEvent in domainEvents)
            {
                INotification? notification = CreateNotification(domainEvent);

                if (notification == null)
                {
                    continue;
                }

                await this._publisher.Publish(notification);
            }

            static INotification? CreateNotification(IDomainEvent domainEvent)
            {
                Type notificationType = typeof(MediatRDomainEventNotification<>).MakeGenericType(domainEvent.GetType());

                return (INotification?)Activator.CreateInstance(notificationType, domainEvent);
            }
        }
    }
}
