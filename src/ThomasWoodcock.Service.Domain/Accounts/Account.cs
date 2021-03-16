using System;

using ThomasWoodcock.Service.Domain.Accounts.DomainEvents;
using ThomasWoodcock.Service.Domain.Accounts.FailureReasons;
using ThomasWoodcock.Service.Domain.SeedWork;
using ThomasWoodcock.Service.Domain.SharedKernel;
using ThomasWoodcock.Service.Domain.SharedKernel.Results;

namespace ThomasWoodcock.Service.Domain.Accounts
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents an account.
    /// </summary>
    public sealed class Account : Entity
    {
        /// <inheritdoc />
        private Account()
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="Account" /> class.
        /// </summary>
        /// <param name="id">
        ///     The ID of the account.
        /// </param>
        /// <param name="name">
        ///     The name associated with the account.
        /// </param>
        /// <param name="emailAddress">
        ///     The email address associated with the account.
        /// </param>
        /// <param name="password">
        ///     The password for the account.
        /// </param>
        private Account(Guid id, string name, EmailAddress emailAddress, string password)
            : base(id)
        {
            this.Name = name;
            this.EmailAddress = emailAddress;
            this.Password = password;
            this.IsActive = false;

            this.RaiseDomainEvent(new AccountCreatedEvent(this));
        }

        /// <summary>
        ///     Gets the name associated with the account.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Gets the email address associated with the account.
        /// </summary>
        public EmailAddress EmailAddress { get; }

        /// <summary>
        ///     Gets the password for the account.
        /// </summary>
        public string Password { get; }

        /// <summary>
        ///     Gets the value that determines whether the account is active.
        /// </summary>
        public bool IsActive { get; private set; }

        /// <summary>
        ///     Activates the account.
        /// </summary>
        /// <returns>
        ///     An <see cref="IResult" /> indicating whether the account was successfully activated.
        /// </returns>
        public IResult Activate()
        {
            if (this.IsActive)
            {
                return Result.Failure(new AccountAlreadyActiveFailure());
            }

            this.IsActive = true;
            this.RaiseDomainEvent(new AccountActivatedEvent(this));

            return Result.Success();
        }

        /// <summary>
        ///     Creates an account.
        /// </summary>
        /// <param name="id">
        ///     The ID to associate with the account.
        /// </param>
        /// <param name="name">
        ///     The name to associate with the account.
        /// </param>
        /// <param name="emailAddress">
        ///     The email address to associate with the account.
        /// </param>
        /// <param name="password">
        ///     The password to associate with the account.
        /// </param>
        /// <returns>
        ///     An <see cref="IResult{T}" /> of type <see cref="Account" />.
        /// </returns>
        public static IResult<Account> Create(Guid id, string name, string emailAddress, string password)
        {
            if (id == Guid.Empty)
            {
                return Result.Failure<Account>(new InvalidAccountIdFailure());
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                return Result.Failure<Account>(new InvalidAccountNameFailure());
            }

            IResult<EmailAddress> emailResult = EmailAddress.Create(emailAddress);

            if (emailResult.IsFailed || emailResult.Value == null)
            {
                return Result.Failure<Account>(new InvalidAccountEmailFailure());
            }

            return string.IsNullOrWhiteSpace(password)
                ? Result.Failure<Account>(new InvalidAccountPasswordFailure())
                : Result.Success(new Account(id, name, emailResult.Value, password));
        }
    }
}
