using System;

using ThomasWoodcock.Service.Application.Common.Commands;

namespace ThomasWoodcock.Service.Application.Accounts.Commands.CreateAccount
{
    /// <inheritdoc cref="ICommand" />
    /// <summary>
    ///     Represents a command to create an account.
    /// </summary>
    /// <param name="Id">
    ///     The ID to associate with the account.
    /// </param>
    /// <param name="Name">
    ///     The name to associate with the account.
    /// </param>
    /// <param name="EmailAddress">
    ///     The email address to associate with the account.
    /// </param>
    /// <param name="Password">
    ///     The password to associate with the account.
    /// </param>
    internal sealed record CreateAccountCommand(Guid Id, string Name, string EmailAddress, string Password) : ICommand;
}
