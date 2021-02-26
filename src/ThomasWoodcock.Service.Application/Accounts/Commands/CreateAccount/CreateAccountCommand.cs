using System;

using ThomasWoodcock.Service.Application.Common.Commands;

namespace ThomasWoodcock.Service.Application.Accounts.Commands.CreateAccount
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents a command to create an account.
    /// </summary>
    internal sealed class CreateAccountCommand : ICommand
    {
        /// <summary>
        ///     Gets or sets the ID to associate with the account.
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        ///     Gets or sets the name to associate with the account.
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        ///     Gets or sets the email address to associate with the account.
        /// </summary>
        public string EmailAddress { get; init; }

        /// <summary>
        ///     Gets or sets the password to associate with the account.
        /// </summary>
        public string Password { get; init; }
    }
}
