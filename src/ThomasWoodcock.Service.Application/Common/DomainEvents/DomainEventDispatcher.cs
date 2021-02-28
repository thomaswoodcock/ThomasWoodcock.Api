using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ThomasWoodcock.Service.Domain.SeedWork;

namespace ThomasWoodcock.Service.Application.Common.DomainEvents
{
    /// <inheritdoc />
    /// <summary>
    ///     A concrete implementation of the <see cref="IDomainEventDispatcher" /> class.
    /// </summary>
    internal sealed class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IEnumerable<IDomainEventHandler> _eventHandlers;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DomainEventDispatcher" /> class.
        /// </summary>
        /// <param name="eventHandlers">
        ///     The event handlers to which domain events will be dispatched.
        /// </param>
        public DomainEventDispatcher(IEnumerable<IDomainEventHandler> eventHandlers)
        {
            this._eventHandlers = eventHandlers ?? throw new ArgumentNullException(nameof(eventHandlers));
        }

        /// <inheritdoc />
        public Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents)
        {
            if (domainEvents == null)
            {
                throw new ArgumentNullException(nameof(domainEvents));
            }

            foreach (IDomainEvent domainEvent in domainEvents)
            {
                Type handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());

                IEnumerable<IDomainEventHandler> handlers = this._eventHandlers.Where(handler => handler.GetType()
                    .IsAssignableTo(handlerType));

                foreach (IDomainEventHandler eventHandler in handlers)
                {
                    Task.Run(() => eventHandler.HandleAsync(domainEvent));
                }
            }

            return Task.CompletedTask;
        }
    }
}
