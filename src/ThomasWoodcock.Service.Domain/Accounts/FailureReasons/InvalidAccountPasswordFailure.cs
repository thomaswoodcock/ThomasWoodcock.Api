using ThomasWoodcock.Service.Domain.SharedKernel.Results.FailureReasons;

namespace ThomasWoodcock.Service.Domain.Accounts.FailureReasons
{
    /// <inheritdoc />
    /// <summary>
    ///     An extension of the <see cref="InvalidFormatFailure" /> class that represents a failure that occurs when attempting
    ///     to create an account with an invalid password.
    /// </summary>
    internal sealed class InvalidAccountPasswordFailure : InvalidFormatFailure
    {
    }
}
