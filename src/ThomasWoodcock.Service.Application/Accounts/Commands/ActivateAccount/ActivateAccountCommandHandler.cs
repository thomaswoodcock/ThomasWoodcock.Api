using System;
using System.Threading.Tasks;

using ThomasWoodcock.Service.Application.Accounts.Entities;
using ThomasWoodcock.Service.Application.Accounts.FailureReasons;
using ThomasWoodcock.Service.Application.Common;
using ThomasWoodcock.Service.Domain.Accounts;
using ThomasWoodcock.Service.Domain.Accounts.FailureReasons;
using ThomasWoodcock.Service.Domain.SharedKernel.Results;

namespace ThomasWoodcock.Service.Application.Accounts.Commands.ActivateAccount
{
    /// <summary>
    ///     A handler for <see cref="ActivateAccountCommand" /> objects.
    /// </summary>
    internal sealed class ActivateAccountCommandHandler
    {
        private readonly IAccountCommandRepository _accountRepository;
        private readonly IDomainEventDispatcher _dispatcher;
        private readonly IAccountActivationKeyRepository _keyRepository;
        private readonly IActivateAccountCommandValidator _validator;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActivateAccountCommandHandler" /> class.
        /// </summary>
        /// <param name="validator">
        ///     The <see cref="IActivateAccountCommandValidator" /> used to validate the command.
        /// </param>
        /// <param name="accountRepository">
        ///     The <see cref="IAccountCommandRepository" /> used to retrieve and update accounts.
        /// </param>
        /// <param name="keyRepository">
        ///     The <see cref="IAccountActivationKeyRepository" /> used to retrieve account activation keys.
        /// </param>
        /// <param name="dispatcher">
        ///     The <see cref="IDomainEventDispatcher" /> used to dispatch domain events.
        /// </param>
        public ActivateAccountCommandHandler(IActivateAccountCommandValidator validator,
            IAccountCommandRepository accountRepository, IAccountActivationKeyRepository keyRepository,
            IDomainEventDispatcher dispatcher)
        {
            this._validator = validator ?? throw new ArgumentNullException(nameof(validator));
            this._accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            this._keyRepository = keyRepository ?? throw new ArgumentNullException(nameof(keyRepository));
            this._dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
        }

        /// <summary>
        ///     Handles the given <paramref name="command" />.
        /// </summary>
        /// <param name="command">
        ///     The command to handle.
        /// </param>
        /// <returns>
        ///     An <see cref="IResult" /> that determines whether the account was activated.
        /// </returns>
        internal async Task<IResult> HandleAsync(ActivateAccountCommand command)
        {
            IResult validationResult = this._validator.Validate(command);

            if (validationResult.IsFailed)
            {
                return validationResult;
            }

            Account account = await this._accountRepository.GetAsync(command.AccountId);

            if (account == null)
            {
                return Result.Failure(new AccountDoesNotExistFailure());
            }

            AccountActivationKey activationKey = await this._keyRepository.GetAsync(account);

            if (activationKey == null)
            {
                return Result.Failure(new ActivationKeyDoesNotExistFailure());
            }

            if (activationKey.Value != command.ActivationKey)
            {
                return Result.Failure(new IncorrectActivationKeyFailure());
            }

            IResult activationResult = account.Activate();

            if (activationResult.IsFailed)
            {
                return activationResult;
            }

            await this._accountRepository.SaveAsync();

            await this._dispatcher.DispatchAsync(account.DomainEvents);

            return Result.Success();
        }
    }
}
