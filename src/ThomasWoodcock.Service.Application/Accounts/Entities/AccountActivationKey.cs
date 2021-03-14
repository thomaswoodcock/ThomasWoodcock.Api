using System;

namespace ThomasWoodcock.Service.Application.Accounts.Entities
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents a key that can be used to activate an account.
    /// </summary>
    /// <param name="Value">
    ///     The value of the activation key.
    /// </param>
    public sealed record AccountActivationKey(Guid Value);
}
