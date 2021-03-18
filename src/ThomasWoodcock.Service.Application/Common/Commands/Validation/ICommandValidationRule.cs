using ThomasWoodcock.Service.Domain.SharedKernel.Results;

namespace ThomasWoodcock.Service.Application.Common.Commands.Validation
{
    /// <summary>
    ///     Allows a class to act as a validation rule for a command.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of command that the rule will validate.
    /// </typeparam>
    internal interface ICommandValidationRule<in T>
        where T : class, ICommand
    {
        /// <summary>
        ///     Checks the rule against a given <paramref name="command" />.
        /// </summary>
        /// <param name="command">
        ///     The command that will be checked against the validation rule.
        /// </param>
        /// <returns>
        ///     An <see cref="IResult" /> that determines where the command passed the validation rule.
        /// </returns>
        IResult Check(T command);
    }
}
