using System;
using System.Threading.Tasks;

using ThomasWoodcock.Service.Application.Accounts.Entities;
using ThomasWoodcock.Service.Application.Accounts.FailureReasons;
using ThomasWoodcock.Service.Application.Common.Commands;
using ThomasWoodcock.Service.Application.Common.DomainEvents;
using ThomasWoodcock.Service.Domain.Accounts;
using ThomasWoodcock.Service.Domain.Accounts.FailureReasons;
using ThomasWoodcock.Service.Domain.SharedKernel.Results;

namespace ThomasWoodcock.Service.Application.Accounts.Commands.ActivateAccount
{
    /// <inheritdoc />
    /// <summary>
    ///     A handler for <see cref="ActivateAccountCommand" /> objects.
    /// </summary>
    internal sealed class ActivateAccountCommandHandler : ICommandHandler<ActivateAccountCommand>
    {
        private readonly IAccountCommandRepository _accountRepository;
        private readonly IAccountActivationKeyRepository _keyRepository;
        private readonly IDomainEventPublisher _publisher;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActivateAccountCommandHandler" /> class.
        /// </summary>
        /// <param name="accountRepository">
        ///     The <see cref="IAccountCommandRepository" /> used to retrieve and update accounts.
        /// </param>
        /// <param name="keyRepository">
        ///     The <see cref="IAccountActivationKeyRepository" /> used to retrieve account activation keys.
        /// </param>
        /// <param name="publisher">
        ///     The <see cref="IDomainEventPublisher" /> used to publish domain events.
        /// </param>
        public ActivateAccountCommandHandler(IAccountCommandRepository accountRepository,
            IAccountActivationKeyRepository keyRepository, IDomainEventPublisher publisher)
        {
            this._accountRepository = accountRepository;
            this._keyRepository = keyRepository;
            this._publisher = publisher;
        }

        /// <inheritdoc />
        public async Task<IResult> HandleAsync(ActivateAccountCommand command)
        {
            (Guid accountId, Guid activationKey) = command;

            Account? account = await this._accountRepository.GetAsync(accountId);

            if (account == null)
            {
                return Result.Failure(new AccountDoesNotExistFailure());
            }

            AccountActivationKey? existingActivationKey = await this._keyRepository.GetAsync(account);

            if (existingActivationKey == null)
            {
                return Result.Failure(new ActivationKeyDoesNotExistFailure());
            }

            if (existingActivationKey.Value != activationKey)
            {
                return Result.Failure(new IncorrectActivationKeyFailure());
            }

            IResult activationResult = account.Activate();

            if (activationResult.IsFailed)
            {
                return activationResult;
            }

            await this._accountRepository.SaveAsync();

            await this._publisher.PublishAsync(account.DomainEvents);

            return Result.Success();
        }
    }
}
