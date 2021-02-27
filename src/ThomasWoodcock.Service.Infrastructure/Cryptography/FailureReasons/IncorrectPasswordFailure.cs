using ThomasWoodcock.Service.Domain.SharedKernel.Results.FailureReasons;

namespace ThomasWoodcock.Service.Infrastructure.Cryptography.FailureReasons
{
    /// <inheritdoc />
    /// <summary>
    ///     An extension of the <see cref="InsufficientPermissionsFailure" /> class that represents a failure that occurs when
    ///     an incorrect password has been provided.
    /// </summary>
    internal sealed class IncorrectPasswordFailure : InsufficientPermissionsFailure
    {
    }
}
