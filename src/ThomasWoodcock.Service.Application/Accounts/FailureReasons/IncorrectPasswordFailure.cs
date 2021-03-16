using ThomasWoodcock.Service.Domain.SharedKernel.Results.FailureReasons;

namespace ThomasWoodcock.Service.Application.Accounts.FailureReasons
{
    /// <inheritdoc cref="InsufficientPermissionsFailure" />
    /// <summary>
    ///     An extension of the <see cref="InsufficientPermissionsFailure" /> record that represents a failure that occurs when
    ///     an incorrect password has been provided.
    /// </summary>
    internal sealed record IncorrectPasswordFailure : InsufficientPermissionsFailure;
}
