namespace ThomasWoodcock.Service.Domain.SharedKernel.Results.FailureReasons
{
    /// <inheritdoc cref="IFailureReason" />
    /// <summary>
    ///     An implementation of the <see cref="IFailureReason" /> interface that represents a failure that occurs when data is
    ///     in the incorrect format.
    /// </summary>
    public record InvalidFormatFailure : IFailureReason;
}
