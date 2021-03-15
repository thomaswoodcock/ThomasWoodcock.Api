namespace ThomasWoodcock.Service.Application.Common.Commands.Validation
{
    /// <summary>
    ///     Allows a class to act as configuration for an <see cref="ICommandValidator{T}" /> object.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of command whose validators will be configured.
    /// </typeparam>
    internal interface ICommandValidatorConfiguration<T>
        where T : class, ICommand
    {
        /// <summary>
        ///     Configures validation for the given command type.
        /// </summary>
        /// <param name="builder">
        ///     Provides validation methods for the command.
        /// </param>
        void Configure(ICommandValidatorBuilder<T> builder);
    }
}
