using ThomasWoodcock.Service.Domain.SharedKernel.Results.FailureReasons;

namespace ThomasWoodcock.Service.Domain.Accounts.FailureReasons
{
    /// <inheritdoc cref="InvalidOperationFailure" />
    /// <summary>
    ///     An extension of the <see cref="InvalidOperationFailure" /> record that represents a failure that occurs when an
    ///     account could not be created.
    /// </summary>
    public sealed record AccountCreationFailure : InvalidOperationFailure;
}
