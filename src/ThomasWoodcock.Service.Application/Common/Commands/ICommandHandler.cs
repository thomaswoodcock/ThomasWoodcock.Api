using System.Threading;
using System.Threading.Tasks;

using ThomasWoodcock.Service.Application.Common.Requests;
using ThomasWoodcock.Service.Domain.SharedKernel.Results;

namespace ThomasWoodcock.Service.Application.Common.Commands
{
    /// <inheritdoc />
    /// <summary>
    ///     Allows a class to act as a handler for application commands.
    /// </summary>
    /// <typeparam name="T">
    ///     The <see cref="ICommand" /> type that the handler will handle.
    /// </typeparam>
    internal interface ICommandHandler<in T> : IRequestHandler<T, IResult>
        where T : class, ICommand
    {
        /// <inheritdoc />
        Task<IResult> IRequestHandler<T, IResult>.HandleAsync(T request, CancellationToken cancellationToken) =>
            this.HandleAsync(request);

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
