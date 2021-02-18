using System;

using ThomasWoodcock.Service.Domain.SeedWork;

namespace ThomasWoodcock.Service.Domain.Accounts.DomainEvents
{
    /// <inheritdoc />
    /// <summary>
    ///     An implementation of the <see cref="IDomainEvent" /> interface that represents a domain event that is raised when
    ///     an account has been activated.
    /// </summary>
    public sealed class AccountActivatedEvent : IDomainEvent
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AccountActivatedEvent" /> class.
        /// </summary>
        /// <param name="account">
        ///     The account that was activated.
        /// </param>
        internal AccountActivatedEvent(Account account)
        {
            this.Account = account ?? throw new ArgumentNullException(nameof(account));
        }

        /// <summary>
        ///     Gets the account that was activated.
        /// </summary>
        public Account Account { get; }
    }
}
