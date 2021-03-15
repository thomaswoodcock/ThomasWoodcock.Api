using ThomasWoodcock.Service.Domain.SharedKernel.Results.FailureReasons;

namespace ThomasWoodcock.Service.Domain.Accounts.FailureReasons
{
    /// <inheritdoc cref="EntityExistsFailure" />
    /// <summary>
    ///     An extension of the <see cref="EntityExistsFailure" /> record that represents a failure that occurs when an account
    ///     with the same ID already exists.
    /// </summary>
    public sealed record AccountIdExistsFailure : EntityExistsFailure;
}
