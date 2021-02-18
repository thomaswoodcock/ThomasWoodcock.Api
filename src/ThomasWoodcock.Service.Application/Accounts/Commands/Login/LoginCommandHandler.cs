using System;
using System.Threading.Tasks;

using ThomasWoodcock.Service.Application.Common;
using ThomasWoodcock.Service.Application.Common.Commands;
using ThomasWoodcock.Service.Domain.Accounts;
using ThomasWoodcock.Service.Domain.Accounts.FailureReasons;
using ThomasWoodcock.Service.Domain.SharedKernel.Results;

namespace ThomasWoodcock.Service.Application.Accounts.Commands.Login
{
    /// <inheritdoc />
    /// <summary>
    ///     An implementation of the <see cref="ICommandHandler{T}" /> interface used to handle <see cref="LoginCommand" />
    ///     objects.
    /// </summary>
    internal sealed class LoginCommandHandler : ICommandHandler<LoginCommand>
    {
        private readonly IPasswordHasher _hasher;
        private readonly IAccountCommandRepository _repository;
        private readonly ICommandValidator<LoginCommand> _validator;

        /// <summary>
        ///     Initializes a new instance of the <see cref="LoginCommandHandler" /> class.
        /// </summary>
        /// <param name="validator">
        ///     The <see cref="ICommandValidator{T}" /> used to validate the command.
        /// </param>
        /// <param name="repository">
        ///     The <see cref="IAccountCommandRepository" /> used to retrieve accounts.
        /// </param>
        /// <param name="hasher">
        ///     The <see cref="IPasswordHasher" /> used to verify passwords.
        /// </param>
        public LoginCommandHandler(ICommandValidator<LoginCommand> validator, IAccountCommandRepository repository,
            IPasswordHasher hasher)
        {
            this._validator = validator ?? throw new ArgumentNullException(nameof(validator));
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this._hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
        }

        /// <inheritdoc />
        public async Task<IResult> HandleAsync(LoginCommand command)
        {
            IResult validationResult = this._validator.Validate(command);

            if (validationResult.IsFailed)
            {
                return validationResult;
            }

            Account account = await this._repository.GetAsync(command.EmailAddress);

            if (account == null)
            {
                return Result.Failure(new AccountDoesNotExistFailure());
            }

            IResult verificationResult = this._hasher.Verify(account.Password, command.Password);

            return verificationResult.IsFailed ? verificationResult : Result.Success();
        }
    }
}
