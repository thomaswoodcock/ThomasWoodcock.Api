using ThomasWoodcock.Service.Domain.SharedKernel.Results.FailureReasons;

namespace ThomasWoodcock.Service.Domain.SharedKernel.Results
{
    /// <summary>
    ///     A static class that provides methods for creating result objects.
    /// </summary>
    public static class Result
    {
        /// <summary>
        ///     Creates a successful result.
        /// </summary>
        /// <returns>
        ///     An <see cref="IResult" /> that represents a successful result.
        /// </returns>
        public static IResult Success() => new SuccessfulResult();

        /// <summary>
        ///     Creates a successful result that contains a value.
        /// </summary>
        /// <param name="value">
        ///     The value contained within the result.
        /// </param>
        /// <typeparam name="T">
        ///     The type of value contained within the result.
        /// </typeparam>
        /// <returns>
        ///     An <see cref="IResult{T}" /> that represents a successful result that contains a value.
        /// </returns>
        internal static IResult<T> Success<T>(T value) => new SuccessfulResult<T>(value);

        /// <summary>
        ///     Creates a failed result.
        /// </summary>
        /// <param name="reason">
        ///     The reason that the operation failed.
        /// </param>
        /// <returns>
        ///     An <see cref="IResult" /> that represents a failed result.
        /// </returns>
        public static IResult Failure(IFailureReason reason) => new FailedResult(reason);

        /// <summary>
        ///     Creates a failed result that would have contained a value had the operation been successful.
        /// </summary>
        /// <param name="reason">
        ///     The reason that the operation failed.
        /// </param>
        /// <typeparam name="T">
        ///     The type of value that would have been contained within the result.
        /// </typeparam>
        /// <returns>
        ///     An <see cref="IResult{T}" /> that represents a failed result that would have contained a value had the operation
        ///     been successful
        /// </returns>
        internal static IResult<T> Failure<T>(IFailureReason reason) => new FailedResult<T>(reason);
    }
}
