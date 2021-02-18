using ThomasWoodcock.Service.Domain.SharedKernel.Results.FailureReasons;

namespace ThomasWoodcock.Service.Application.Accounts.FailureReasons
{
    /// <inheritdoc />
    /// <summary>
    ///     An extension of the <see cref="InsufficientPermissionsFailure" /> class that represents a failure that occurs when
    ///     an incorrect activation key has been provided.
    /// </summary>
    internal sealed class IncorrectActivationKeyFailure : InsufficientPermissionsFailure
    {
    }
}
