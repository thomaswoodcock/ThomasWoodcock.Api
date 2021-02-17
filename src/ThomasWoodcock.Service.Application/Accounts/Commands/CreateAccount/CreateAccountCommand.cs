using System;

namespace ThomasWoodcock.Service.Application.Accounts.Commands.CreateAccount
{
    /// <summary>
    ///     Represents a command to create an account.
    /// </summary>
    public sealed class CreateAccountCommand
    {
        /// <summary>
        ///     Gets or sets the ID to associate with the account.
        /// </summary>
        internal Guid Id { get; init; }

        /// <summary>
        ///     Gets or sets the name to associate with the account.
        /// </summary>
        internal string Name { get; init; }

        /// <summary>
        ///     Gets or sets the email address to associate with the account.
        /// </summary>
        internal string EmailAddress { get; init; }

        /// <summary>
        ///     Gets or sets the password to associate with the account.
        /// </summary>
        internal string Password { get; init; }
    }
}
