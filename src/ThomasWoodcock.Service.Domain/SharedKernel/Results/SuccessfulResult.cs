using ThomasWoodcock.Service.Domain.SharedKernel.Results.FailureReasons;

namespace ThomasWoodcock.Service.Domain.SharedKernel.Results
{
    /// <inheritdoc cref="IResult" />
    /// <summary>
    ///     An implementation of the <see cref="IResult" /> interface that represents a successful result.
    /// </summary>
    internal record SuccessfulResult : IResult
    {
        /// <inheritdoc />
        public IFailureReason? FailureReason { get; } = null;
    }

    /// <inheritdoc cref="SuccessfulResult" />
    /// <inheritdoc cref="IResult{T}" />
    /// <summary>
    ///     An implementation of the <see cref="IResult{T}" /> interface that represents a successful result that contains a
    ///     value.
    /// </summary>
    /// <param name="Value">
    ///     The value contained within the result.
    /// </param>
    internal sealed record SuccessfulResult<T>(T Value) : SuccessfulResult, IResult<T>;
}
