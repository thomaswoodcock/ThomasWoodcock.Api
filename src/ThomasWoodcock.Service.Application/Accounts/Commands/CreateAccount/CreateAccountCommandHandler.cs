using System;
using System.Threading.Tasks;

using ThomasWoodcock.Service.Application.Common;
using ThomasWoodcock.Service.Domain.Accounts;
using ThomasWoodcock.Service.Domain.Accounts.FailureReasons;
using ThomasWoodcock.Service.Domain.SharedKernel.Results;

namespace ThomasWoodcock.Service.Application.Accounts.Commands.CreateAccount
{
    /// <summary>
    ///     A handler for <see cref="CreateAccountCommand" /> objects.
    /// </summary>
    internal sealed class CreateAccountCommandHandler
    {
        private readonly IDomainEventDispatcher _dispatcher;
        private readonly IPasswordHasher _hasher;
        private readonly IAccountCommandRepository _repository;
        private readonly ICreateAccountCommandValidator _validator;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CreateAccountCommandHandler" /> class.
        /// </summary>
        /// <param name="validator">
        ///     The <see cref="ICreateAccountCommandValidator" /> used to validate the command.
        /// </param>
        /// <param name="repository">
        ///     The <see cref="IAccountCommandRepository" /> used to retrieve and add accounts.
        /// </param>
        /// <param name="hasher">
        ///     The <see cref="IPasswordHasher" /> used to hash passwords.
        /// </param>
        /// <param name="dispatcher">
        ///     The <see cref="IDomainEventDispatcher" /> used to dispatch domain events.
        /// </param>
        public CreateAccountCommandHandler(ICreateAccountCommandValidator validator,
            IAccountCommandRepository repository, IPasswordHasher hasher, IDomainEventDispatcher dispatcher)
        {
            this._validator = validator ?? throw new ArgumentNullException(nameof(validator));
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this._hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
            this._dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
        }

        /// <summary>
        ///     Handles the given <paramref name="command" />.
        /// </summary>
        /// <param name="command">
        ///     The command to handle.
        /// </param>
        /// <returns>
        ///     An <see cref="IResult" /> that determines whether the account was created.
        /// </returns>
        public async Task<IResult> HandleAsync(CreateAccountCommand command)
        {
            IResult validationResult = this._validator.Validate(command);

            if (validationResult.IsFailed)
            {
                return validationResult;
            }

            Account accountById = await this._repository.GetAsync(command.Id);

            if (accountById != null)
            {
                return Result.Failure(new AccountIdExistsFailure());
            }

            Account accountByEmail = await this._repository.GetAsync(command.EmailAddress);

            if (accountByEmail != null)
            {
                return Result.Failure(new AccountEmailExistsFailure());
            }

            IResult<Account> accountResult = Account.Create(command.Id, command.Name, command.EmailAddress,
                this._hasher.Hash(command.Password));

            if (accountResult.IsFailed)
            {
                return accountResult;
            }

            this._repository.Add(accountResult.Value);
            await this._repository.SaveAsync();

            await this._dispatcher.DispatchAsync(accountResult.Value.DomainEvents);

            return Result.Success();
        }
    }
}
