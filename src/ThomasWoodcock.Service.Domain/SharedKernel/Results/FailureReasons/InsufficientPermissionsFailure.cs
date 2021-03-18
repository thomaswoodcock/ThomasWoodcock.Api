namespace ThomasWoodcock.Service.Domain.SharedKernel.Results.FailureReasons
{
    /// <inheritdoc cref="IFailureReason" />
    /// <summary>
    ///     An implementation of the <see cref="IFailureReason" /> interface that represents a failure that occurs when an
    ///     operation cannot be executed due to insufficient permissions.
    /// </summary>
    public record InsufficientPermissionsFailure : IFailureReason;
}
