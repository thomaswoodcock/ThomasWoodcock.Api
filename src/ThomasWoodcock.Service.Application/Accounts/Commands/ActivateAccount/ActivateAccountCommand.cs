using System;

using ThomasWoodcock.Service.Application.Common.Commands;

namespace ThomasWoodcock.Service.Application.Accounts.Commands.ActivateAccount
{
    /// <inheritdoc cref="ICommand" />
    /// <summary>
    ///     Represents a command to activate an account.
    /// </summary>
    /// <param name="AccountId">
    ///     The ID of the account to activate.
    /// </param>
    /// <param name="ActivationKey">
    ///     The key with which to activate the account.
    /// </param>
    internal sealed record ActivateAccountCommand(Guid AccountId, Guid ActivationKey) : ICommand;
}
