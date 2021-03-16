using System;
using System.Threading.Tasks;

using ThomasWoodcock.Service.Application.Accounts.Commands;
using ThomasWoodcock.Service.Application.Accounts.Entities;
using ThomasWoodcock.Service.Application.Accounts.Notifications;
using ThomasWoodcock.Service.Application.Common.DomainEvents;
using ThomasWoodcock.Service.Application.Common.Notifications;
using ThomasWoodcock.Service.Domain.Accounts.DomainEvents;

namespace ThomasWoodcock.Service.Application.Accounts.EventHandlers
{
    /// <inheritdoc />
    /// <summary>
    ///     A handler for <see cref="AccountCreatedEvent" /> objects.
    /// </summary>
    internal sealed class AccountCreatedEventHandler : IDomainEventHandler<AccountCreatedEvent>
    {
        private readonly IAccountActivationKeyRepository _repository;
        private readonly INotificationSender _sender;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AccountCreatedEventHandler" /> class.
        /// </summary>
        /// <param name="repository">
        ///     The <see cref="IAccountActivationKeyRepository" /> used to add activation keys.
        /// </param>
        /// <param name="sender">
        ///     The <see cref="INotificationSender" /> used to send a notification to the user.
        /// </param>
        public AccountCreatedEventHandler(IAccountActivationKeyRepository repository, INotificationSender sender)
        {
            this._repository = repository;
            this._sender = sender;
        }

        /// <inheritdoc />
        public async Task HandleAsync(AccountCreatedEvent createdEvent)
        {
            AccountActivationKey activationKey = new(Guid.NewGuid());

            this._repository.Add(createdEvent.Account, activationKey);
            await this._repository.SaveAsync();

            await this._sender.SendAsync(new AccountActivationNotification(createdEvent.Account, activationKey));
        }
    }
}
