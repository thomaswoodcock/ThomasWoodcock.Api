using System.Threading.Tasks;

using ThomasWoodcock.Service.Domain.SharedKernel.Results;

namespace ThomasWoodcock.Service.Application.Common.Commands
{
    /// <summary>
    ///     Allows a class to act as a handler for application commands.
    /// </summary>
    internal interface ICommandHandler
    {
        /// <summary>
        ///     Handles the given <paramref name="command" />.
        /// </summary>
        /// <param name="command">
        ///     The command to handle.
        /// </param>
        /// <returns>
        ///     An <see cref="IResult" /> that indicates whether the command was handled successfully.
        /// </returns>
        Task<IResult> HandleAsync(ICommand command);
    }

    /// <inheritdoc />
    /// <summary>
    ///     Allows a class to act as a handler for application commands.
    /// </summary>
    /// <typeparam name="T">
    ///     The <see cref="ICommand" /> type that the handler will handle.
    /// </typeparam>
    internal interface ICommandHandler<in T> : ICommandHandler
        where T : class, ICommand
    {
        /// <inheritdoc />
        Task<IResult> ICommandHandler.HandleAsync(ICommand command) => this.HandleAsync((T)command);

        /// <summary>
        ///     Handles the given <paramref name="command" />.
        /// </summary>
        /// <param name="command">
        ///     The command to handle.
        /// </param>
        /// <returns>
        ///     An <see cref="IResult" /> that indicates whether the command was handled successfully.
        /// </returns>
        Task<IResult> HandleAsync(T command);
    }
}
