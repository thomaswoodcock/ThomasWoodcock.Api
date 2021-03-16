using System;
using System.Collections.Generic;

namespace ThomasWoodcock.Service.Domain.SeedWork
{
    /// <summary>
    ///     Allows a class to act as a domain entity.
    /// </summary>
    public abstract class Entity
    {
        private readonly List<IDomainEvent> _domainEvents = new();

        /// <summary>
        ///     Initializes a new instance of the <see cref="Entity" /> class.
        /// </summary>
        /// <param name="id">
        ///     The ID of the domain entity.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if the given <paramref name="id" /> is an empty <see cref="Guid" />.
        /// </exception>
        private protected Entity(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }

            this.Id = id;
        }

        /// <summary>
        ///     Gets the ID of the entity.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        ///     Gets the domain events that the entity has raised.
        /// </summary>
        public IEnumerable<IDomainEvent> DomainEvents => this._domainEvents;

        /// <summary>
        ///     Raises a domain event.
        /// </summary>
        /// <param name="domainEvent">
        ///     The domain event to raise.
        /// </param>
        private protected void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            this._domainEvents.Add(domainEvent);
        }
    }
}
