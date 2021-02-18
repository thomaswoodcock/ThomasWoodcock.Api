using ThomasWoodcock.Service.Domain.SharedKernel.Results.FailureReasons;

namespace ThomasWoodcock.Service.Domain.Accounts.FailureReasons
{
    /// <inheritdoc />
    /// <summary>
    ///     An extension of the <see cref="InvalidOperationFailure" /> class that represents a failure that occurs when an
    ///     account is already active.
    /// </summary>
    public sealed class AccountAlreadyActiveFailure : InvalidOperationFailure
    {
    }
}
