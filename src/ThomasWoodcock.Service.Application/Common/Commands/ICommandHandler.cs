using System.Threading.Tasks;

using ThomasWoodcock.Service.Domain.SharedKernel.Results;

namespace ThomasWoodcock.Service.Application.Common.Commands
{
    /// <summary>
    ///     Allows a class to act as a handler for application commands.
    /// </summary>
    /// <typeparam name="T">
    ///     The <see cref="ICommand" /> type that the handler will handle.
    /// </typeparam>
    public interface ICommandHandler<in T>
        where T : ICommand
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
        Task<IResult> HandleAsync(T command);
    }
}
