using System;
using System.Threading.Tasks;

using ThomasWoodcock.Service.Application.Accounts.Notifications;
using ThomasWoodcock.Service.Application.Accounts.Services;
using ThomasWoodcock.Service.Application.Common.Notifications;
using ThomasWoodcock.Service.Domain.Accounts.DomainEvents;

namespace ThomasWoodcock.Service.Application.Accounts.EventHandlers
{
    /// <summary>
    ///     A handler for <see cref="AccountCreatedEvent" /> objects.
    /// </summary>
    internal sealed class AccountCreatedEventHandler
    {
        private readonly IAccountActivationKeyGenerator _keyGenerator;
        private readonly INotificationSender _sender;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AccountCreatedEventHandler" /> class.
        /// </summary>
        /// <param name="keyGenerator">
        ///     The <see cref="IAccountActivationKeyGenerator" /> used to generate an account activation key.
        /// </param>
        /// <param name="sender">
        ///     The <see cref="INotificationSender" /> used to send a notification to the user.
        /// </param>
        public AccountCreatedEventHandler(IAccountActivationKeyGenerator keyGenerator, INotificationSender sender)
        {
            this._keyGenerator = keyGenerator ?? throw new ArgumentNullException(nameof(keyGenerator));
            this._sender = sender ?? throw new ArgumentNullException(nameof(sender));
        }

        /// <summary>
        ///     Handles the given <paramref name="createdEvent" />.
        /// </summary>
        /// <param name="createdEvent">
        ///     The event to handle.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if the given <paramref name="createdEvent" /> is null.
        /// </exception>
        public async Task HandleAsync(AccountCreatedEvent createdEvent)
        {
            if (createdEvent == null)
            {
                throw new ArgumentNullException(nameof(createdEvent));
            }

            Guid activationKey = await this._keyGenerator.GenerateAsync(createdEvent.Account);
            await this._sender.SendAsync(new AccountActivationNotification(createdEvent.Account, activationKey));
        }
    }
}
