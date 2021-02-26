using System;

using ThomasWoodcock.Service.Domain.SharedKernel.Results.FailureReasons;

namespace ThomasWoodcock.Service.Domain.SharedKernel.Results
{
    /// <inheritdoc />
    /// <summary>
    ///     An implementation of the <see cref="IResult" /> interface that represents a failed result.
    /// </summary>
    internal class FailedResult : IResult
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FailedResult" /> class.
        /// </summary>
        /// <param name="reason">
        ///     The reason that the operation failed.
        /// </param>
        public FailedResult(IFailureReason reason)
        {
            this.FailureReason = reason ?? throw new ArgumentNullException(nameof(reason));
        }

        /// <inheritdoc />
        public bool IsSuccessful { get; } = false;

        /// <inheritdoc />
        public bool IsFailed { get; } = true;

        /// <inheritdoc />
        public IFailureReason FailureReason { get; }
    }

    /// <inheritdoc cref="FailedResult" />
    /// <inheritdoc cref="IResult{T}" />
    /// <summary>
    ///     An implementation of the <see cref="IResult{T}" /> interface that represents a failed result that would have
    ///     contained a value had the operation been successful.
    /// </summary>
    internal sealed class FailedResult<T> : FailedResult, IResult<T>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="FailedResult{T}" /> class.
        /// </summary>
        public FailedResult(IFailureReason reason)
            : base(reason)
        {
        }

        /// <inheritdoc />
        public T Value { get; } = default;
    }
}
