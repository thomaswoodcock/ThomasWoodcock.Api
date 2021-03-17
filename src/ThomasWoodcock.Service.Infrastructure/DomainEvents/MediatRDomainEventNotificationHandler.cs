using System.Threading;
using System.Threading.Tasks;

using MediatR;

using ThomasWoodcock.Service.Application.Common.DomainEvents;
using ThomasWoodcock.Service.Domain.SeedWork;

namespace ThomasWoodcock.Service.Infrastructure.DomainEvents
{
    /// <inheritdoc />
    /// <summary>
    ///     An implementation of the <see cref="INotificationHandler{TNotification}" /> interface used to handle
    ///     <see cref="MediatRDomainEventNotification{T}" /> notifications.
    /// </summary>
    /// <typeparam name="TDomainEvent">
    ///     The type of domain event within the notification.
    /// </typeparam>
    /// <typeparam name="THandler">
    ///     The domain event handler used to handler the domain event within the notification.
    /// </typeparam>
    internal sealed class
        MediatRDomainEventNotificationHandler<TDomainEvent, THandler> : INotificationHandler<
            MediatRDomainEventNotification<TDomainEvent>>
        where TDomainEvent : class, IDomainEvent where THandler : IDomainEventHandler<TDomainEvent>
    {
        private readonly THandler _handler;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MediatRDomainEventNotificationHandler{TDomainEvent,THandler}" />
        ///     class.
        /// </summary>
        /// <param name="handler">
        ///     The <see cref="IDomainEventHandler{T}" /> used to handle the domain event within the notifications.
        /// </param>
        public MediatRDomainEventNotificationHandler(THandler handler)
        {
            this._handler = handler;
        }

        /// <inheritdoc />
        public async Task Handle(MediatRDomainEventNotification<TDomainEvent> notification,
            CancellationToken cancellationToken)
        {
            await this._handler.HandleAsync(notification.DomainEvent);
        }
    }
}
