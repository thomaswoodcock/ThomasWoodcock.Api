using ThomasWoodcock.Service.Domain.SharedKernel.Results.FailureReasons;

namespace ThomasWoodcock.Service.Domain.Accounts.FailureReasons
{
    /// <inheritdoc cref="EntityDoesNotExistFailure" />
    /// <summary>
    ///     An extension of the <see cref="EntityDoesNotExistFailure" /> record that represents a failure that occurs when an
    ///     account does not exist.
    /// </summary>
    public sealed record AccountDoesNotExistFailure : EntityDoesNotExistFailure;
}
