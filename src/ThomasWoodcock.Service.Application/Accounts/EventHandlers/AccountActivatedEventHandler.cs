using System;
using System.Threading.Tasks;

using ThomasWoodcock.Service.Application.Accounts.Commands;
using ThomasWoodcock.Service.Application.Accounts.Entities;
using ThomasWoodcock.Service.Application.Common.DomainEvents;
using ThomasWoodcock.Service.Domain.Accounts.DomainEvents;

namespace ThomasWoodcock.Service.Application.Accounts.EventHandlers
{
    /// <inheritdoc />
    /// <summary>
    ///     A handler for <see cref="AccountActivatedEvent" /> objects.
    /// </summary>
    internal sealed class AccountActivatedEventHandler : IDomainEventHandler<AccountActivatedEvent>
    {
        private readonly IAccountActivationKeyRepository _repository;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AccountActivatedEventHandler" /> class.
        /// </summary>
        /// <param name="repository">
        ///     The <see cref="IAccountActivationKeyRepository" /> used to remove activation keys.
        /// </param>
        public AccountActivatedEventHandler(IAccountActivationKeyRepository repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <inheritdoc />
        public async Task HandleAsync(AccountActivatedEvent activatedEvent)
        {
            if (activatedEvent == null)
            {
                throw new ArgumentNullException(nameof(activatedEvent));
            }

            AccountActivationKey key = await this._repository.GetAsync(activatedEvent.Account);

            if (key == null)
            {
                return;
            }

            this._repository.Remove(key);
            await this._repository.SaveAsync();
        }
    }
}
