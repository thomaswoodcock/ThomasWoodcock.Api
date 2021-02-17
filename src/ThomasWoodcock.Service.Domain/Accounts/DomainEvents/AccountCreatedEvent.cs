using System;

using ThomasWoodcock.Service.Domain.SeedWork;

namespace ThomasWoodcock.Service.Domain.Accounts.DomainEvents
{
    /// <inheritdoc />
    /// <summary>
    ///     An implementation of the <see cref="IDomainEvent" /> interface that represents a domain event that is raised when
    ///     an account has been created.
    /// </summary>
    public sealed class AccountCreatedEvent : IDomainEvent
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AccountCreatedEvent" /> class.
        /// </summary>
        /// <param name="account">
        ///     The account that was created.
        /// </param>
        internal AccountCreatedEvent(Account account)
        {
            this.Account = account ?? throw new ArgumentNullException(nameof(account));
        }

        /// <summary>
        ///     Gets the account that was created.
        /// </summary>
        public Account Account { get; }
    }
}
