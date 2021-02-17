using System;

using ThomasWoodcock.Service.Application.Common.Notifications;
using ThomasWoodcock.Service.Domain.Accounts;

namespace ThomasWoodcock.Service.Application.Accounts.Notifications
{
    /// <inheritdoc />
    /// <summary>
    ///     An implementation of the <see cref="INotification" /> interface used for notifying users that their account needs
    ///     to be activated.
    /// </summary>
    internal sealed class AccountActivationNotification : INotification
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AccountActivationNotification" /> class.
        /// </summary>
        /// <param name="account">
        ///     The account that required activation.
        /// </param>
        /// <param name="activationKey">
        ///     The key that can be used to activate the account.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if the given <paramref name="account" /> is null or <paramref name="activationKey" /> is empty.
        /// </exception>
        public AccountActivationNotification(Account account, Guid activationKey)
        {
            this.Account = account ?? throw new ArgumentNullException(nameof(account));

            if (activationKey == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(activationKey));
            }

            this.ActivationKey = activationKey;
        }

        /// <summary>
        ///     Gets the account that required activation.
        /// </summary>
        public Account Account { get; }

        /// <summary>
        ///     Gets the key that can be used to activate the account.
        /// </summary>
        public Guid ActivationKey { get; }
    }
}
