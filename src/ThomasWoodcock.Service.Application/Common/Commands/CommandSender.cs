using System.Threading;
using System.Threading.Tasks;

using ThomasWoodcock.Service.Application.Common.Requests;
using ThomasWoodcock.Service.Domain.SharedKernel.Results;

namespace ThomasWoodcock.Service.Application.Common.Commands
{
    /// <inheritdoc />
    /// <summary>
    ///     An implementation of the <see cref="ICommandSender" /> interface used to send commands as application requests.
    /// </summary>
    internal sealed class CommandSender : ICommandSender
    {
        private readonly IRequestSender _sender;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandSender" /> class.
        /// </summary>
        /// <param name="sender">
        ///     The request sender used to send application requests.
        /// </param>
        public CommandSender(IRequestSender sender)
        {
            this._sender = sender;
        }

        /// <inheritdoc />
        public async Task<IResult> SendAsync(ICommand command) =>
            await this._sender.SendAsync(command, CancellationToken.None);
    }
}
