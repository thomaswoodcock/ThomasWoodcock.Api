using ThomasWoodcock.Service.Domain.SharedKernel.Results.FailureReasons;

namespace ThomasWoodcock.Service.Application.Common.Commands.FailureReasons
{
    /// <inheritdoc />
    /// <summary>
    ///     An extension of the <see cref="InvalidOperationFailure" /> that represents a failure that occurs when a command is
    ///     sent without having a registered command handler.
    /// </summary>
    internal sealed class UnhandledCommandFailure : InvalidOperationFailure
    {
    }
}
