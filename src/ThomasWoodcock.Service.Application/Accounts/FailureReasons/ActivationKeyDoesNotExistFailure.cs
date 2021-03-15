using ThomasWoodcock.Service.Domain.SharedKernel.Results.FailureReasons;

namespace ThomasWoodcock.Service.Application.Accounts.FailureReasons
{
    /// <inheritdoc cref="EntityDoesNotExistFailure" />
    /// <summary>
    ///     An extension of the <see cref="EntityDoesNotExistFailure" /> record that represents a failure that occurs when an
    ///     account activation key does not exist.
    /// </summary>
    internal sealed record ActivationKeyDoesNotExistFailure : EntityDoesNotExistFailure;
}
