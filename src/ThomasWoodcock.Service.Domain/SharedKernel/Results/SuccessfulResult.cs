using ThomasWoodcock.Service.Domain.SharedKernel.Results.FailureReasons;

namespace ThomasWoodcock.Service.Domain.SharedKernel.Results
{
    /// <inheritdoc />
    /// <summary>
    ///     An implementation of the <see cref="IResult" /> interface that represents a successful result.
    /// </summary>
    internal class SuccessfulResult : IResult
    {
        /// <inheritdoc />
        public bool IsSuccessful { get; } = true;

        /// <inheritdoc />
        public bool IsFailed { get; } = false;

        /// <inheritdoc />
        public IFailureReason FailureReason { get; } = null;
    }

    /// <inheritdoc cref="SuccessfulResult" />
    /// <inheritdoc cref="IResult{T}" />
    /// <summary>
    ///     An implementation of the <see cref="IResult{T}" /> interface that represents a successful result that contains a
    ///     value.
    /// </summary>
    internal sealed class SuccessfulResult<T> : SuccessfulResult, IResult<T>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SuccessfulResult{T}" /> class.
        /// </summary>
        /// <param name="value">
        ///     The value contained within the result.
        /// </param>
        public SuccessfulResult(T value)
        {
            this.Value = value;
        }

        /// <inheritdoc />
        public T Value { get; }
    }
}
