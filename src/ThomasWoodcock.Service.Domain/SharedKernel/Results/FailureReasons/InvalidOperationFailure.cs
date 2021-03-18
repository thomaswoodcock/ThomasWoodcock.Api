namespace ThomasWoodcock.Service.Domain.SharedKernel.Results.FailureReasons
{
    /// <inheritdoc cref="IFailureReason" />
    /// <summary>
    ///     An implementation of the <see cref="IFailureReason" /> interface that represents a failure that occurs when
    ///     attempting to execute an invalid operation.
    /// </summary>
    public record InvalidOperationFailure : IFailureReason;
}
