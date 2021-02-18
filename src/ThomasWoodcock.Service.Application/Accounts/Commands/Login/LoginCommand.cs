using ThomasWoodcock.Service.Application.Common.Commands;

namespace ThomasWoodcock.Service.Application.Accounts.Commands.Login
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents a command to login to an account.
    /// </summary>
    internal sealed class LoginCommand : ICommand
    {
        /// <summary>
        ///     Gets or sets the email address of the account that will be logged in.
        /// </summary>
        internal string EmailAddress { get; init; }

        /// <summary>
        ///     Gets or sets the password for the account that will be logged in.
        /// </summary>
        internal string Password { get; init; }
    }
}
