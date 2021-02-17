using ThomasWoodcock.Service.Domain.SharedKernel.Results.FailureReasons;

namespace ThomasWoodcock.Service.Domain.Accounts.FailureReasons
{
    /// <inheritdoc />
    /// <summary>
    ///     An extension of the <see cref="EntityExistsFailure" /> class that represents a failure that occurs when an account
    ///     with the same ID already exists.
    /// </summary>
    public sealed class AccountIdExistsFailure : EntityExistsFailure
    {
    }
}
