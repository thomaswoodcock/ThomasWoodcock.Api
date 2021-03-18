using ThomasWoodcock.Service.Domain.SharedKernel.Results.FailureReasons;

namespace ThomasWoodcock.Service.Domain.SharedKernel.Results
{
    /// <summary>
    ///     Allows a class to act as the result of executing an operation.
    /// </summary>
    public interface IResult
    {
        /// <summary>
        ///     Gets the value that determines whether the operation was successful.
        /// </summary>
        bool IsSuccessful => !this.IsFailed;

        /// <summary>
        ///     Gets the value that determines whether the operation failed.
        /// </summary>
        bool IsFailed => this.FailureReason != null;

        /// <summary>
        ///     Gets the <see cref="IFailureReason" /> that determines why the operation failed.
        /// </summary>
        IFailureReason? FailureReason { get; }
    }

    /// <inheritdoc />
    /// <typeparam name="T">
    ///     The type of value contained within the result.
    /// </typeparam>
    public interface IResult<out T> : IResult
    {
        /// <summary>
        ///     Gets the value contained within the result.
        /// </summary>
        T? Value { get; }
    }
}
