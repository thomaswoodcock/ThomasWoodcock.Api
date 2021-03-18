using ThomasWoodcock.Service.Domain.SharedKernel.Results.FailureReasons;

namespace ThomasWoodcock.Service.Domain.Accounts.FailureReasons
{
    /// <inheritdoc cref="InvalidFormatFailure" />
    /// <summary>
    ///     An extension of the <see cref="InvalidFormatFailure" /> record that represents a failure that occurs when
    ///     attempting to create an account with an invalid email address.
    /// </summary>
    public sealed record InvalidAccountEmailFailure : InvalidFormatFailure;
}
