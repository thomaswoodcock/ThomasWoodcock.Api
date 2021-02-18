using System;

using ThomasWoodcock.Service.Application.Common.Commands;

namespace ThomasWoodcock.Service.Application.Accounts.Commands.ActivateAccount
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents a command to activate an account.
    /// </summary>
    public sealed class ActivateAccountCommand : ICommand
    {
        /// <summary>
        ///     Gets or sets the ID of the account to activate.
        /// </summary>
        internal Guid AccountId { get; init; }

        /// <summary>
        ///     Gets or sets the key with which to activate the account.
        /// </summary>
        internal Guid ActivationKey { get; init; }
    }
}
