using ThomasWoodcock.Service.Domain.SharedKernel.Results;

namespace ThomasWoodcock.Service.Application.Common.Commands.Validation
{
    /// <summary>
    ///     Allows a class to act as a validator for application commands.
    /// </summary>
    /// <typeparam name="T">
    ///     The <see cref="ICommand" /> type that the validator will validate.
    /// </typeparam>
    internal interface ICommandValidator<in T>
        where T : class, ICommand
    {
        /// <summary>
        ///     Validates the given <paramref name="command" />.
        /// </summary>
        /// <param name="command">
        ///     The command to validate.
        /// </param>
        /// <returns>
        ///     An <see cref="IResult" /> that indicates whether the command was validated successfully.
        /// </returns>
        IResult Validate(T command);
    }
}
