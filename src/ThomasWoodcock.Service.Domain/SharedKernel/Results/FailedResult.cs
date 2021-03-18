using ThomasWoodcock.Service.Domain.SharedKernel.Results.FailureReasons;

namespace ThomasWoodcock.Service.Domain.SharedKernel.Results
{
    /// <inheritdoc cref="IResult" />
    /// <summary>
    ///     An implementation of the <see cref="IResult" /> interface that represents a failed result.
    /// </summary>
    /// <param name="FailureReason">
    ///     The reason that the operation failed.
    /// </param>
    internal record FailedResult(IFailureReason FailureReason) : IResult;

    /// <inheritdoc cref="FailedResult" />
    /// <inheritdoc cref="IResult{T}" />
    /// <summary>
    ///     An implementation of the <see cref="IResult{T}" /> interface that represents a failed result that would have
    ///     contained a value had the operation been successful.
    /// </summary>
    internal sealed record FailedResult<T>(IFailureReason FailureReason) : FailedResult(FailureReason), IResult<T>
    {
        /// <inheritdoc />
        public T? Value { get; } = default;
    }
}
