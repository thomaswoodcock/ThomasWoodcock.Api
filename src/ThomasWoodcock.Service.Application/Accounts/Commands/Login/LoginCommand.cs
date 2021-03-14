using ThomasWoodcock.Service.Application.Common.Commands;

namespace ThomasWoodcock.Service.Application.Accounts.Commands.Login
{
    /// <inheritdoc cref="ICommand" />
    /// <summary>
    ///     Represents a command to login to an account.
    /// </summary>
    /// <param name="EmailAddress">
    ///     The email address of the account that will be logged in.
    /// </param>
    /// <param name="Password">
    ///     The password for the account that will be logged in.
    /// </param>
    internal sealed record LoginCommand(string EmailAddress, string Password) : ICommand;
}
