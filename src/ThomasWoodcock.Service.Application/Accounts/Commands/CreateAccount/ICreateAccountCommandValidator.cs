using ThomasWoodcock.Service.Domain.SharedKernel.Results;

namespace ThomasWoodcock.Service.Application.Accounts.Commands.CreateAccount
{
    /// <summary>
    ///     Allows a class to act as a validator for <see cref="CreateAccountCommand" /> objects.
    /// </summary>
    public interface ICreateAccountCommandValidator
    {
        /// <summary>
        ///     Validates the given <paramref name="command" />.
        /// </summary>
        /// <param name="command">
        ///     The command to validate.
        /// </param>
        /// <returns>
        ///     An <see cref="IResult" /> that determines whether validation was successful.
        /// </returns>
        IResult Validate(CreateAccountCommand command);
    }
}
