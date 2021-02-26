using ThomasWoodcock.Service.Domain.SharedKernel.Results.FailureReasons;

namespace ThomasWoodcock.Service.Application.Common.Commands.FailureReasons
{
    /// <inheritdoc />
    /// <summary>
    ///     An extension of the <see cref="InvalidFormatFailure" /> class that represents a failure that occurs when a command
    ///     is invalid.
    /// </summary>
    internal sealed class InvalidCommandFailure : InvalidFormatFailure
    {
    }
}
