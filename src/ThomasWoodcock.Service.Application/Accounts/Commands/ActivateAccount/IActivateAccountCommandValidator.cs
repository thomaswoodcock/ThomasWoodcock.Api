using ThomasWoodcock.Service.Domain.SharedKernel.Results;

namespace ThomasWoodcock.Service.Application.Accounts.Commands.ActivateAccount
{
    /// <summary>
    ///     Allows a class to act as a validator for <see cref="ActivateAccountCommand" /> objects.
    /// </summary>
    public interface IActivateAccountCommandValidator
    {
        /// <summary>
        ///     Validates the given <paramref name="command" />.
        /// </summary>
        /// <param name="command">
        ///     The command to validate.
        /// </param>
        /// <returns>
        ///     An <see cref="IResult" /> that determines whether the command was validated successfully.
        /// </returns>
        IResult Validate(ActivateAccountCommand command);
    }
}
