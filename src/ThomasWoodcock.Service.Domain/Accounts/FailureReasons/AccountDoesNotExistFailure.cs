using ThomasWoodcock.Service.Domain.SharedKernel.Results.FailureReasons;

namespace ThomasWoodcock.Service.Domain.Accounts.FailureReasons
{
    /// <inheritdoc />
    /// <summary>
    ///     An extension of the <see cref="EntityDoesNotExistFailure" /> class that represents a failure that occurs when an
    ///     account does not exist.
    /// </summary>
    public sealed class AccountDoesNotExistFailure : EntityDoesNotExistFailure
    {
    }
}
