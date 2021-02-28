using System.Threading.Tasks;

using ThomasWoodcock.Service.Domain.SharedKernel.Results;

namespace ThomasWoodcock.Service.Application.Common.Commands
{
    /// <summary>
    ///     Allows a class to act as a service for sending a command to its respective handler.
    /// </summary>
    internal interface ICommandSender
    {
        /// <summary>
        ///     Sends the given <paramref name="command" />.
        /// </summary>
        /// <param name="command">
        ///     The command that will be sent.
        /// </param>
        /// <returns>
        ///     An <see cref="IResult" /> that determines whether the command was handled successfully.
        /// </returns>
        Task<IResult> SendAsync(ICommand command);
    }
}
