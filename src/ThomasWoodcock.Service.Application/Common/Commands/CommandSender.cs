using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ThomasWoodcock.Service.Application.Common.Commands.FailureReasons;
using ThomasWoodcock.Service.Domain.SharedKernel.Results;

namespace ThomasWoodcock.Service.Application.Common.Commands
{
    /// <inheritdoc />
    /// <summary>
    ///     A concrete implementation of the <see cref="ICommandSender" /> interface.
    /// </summary>
    internal sealed class CommandSender : ICommandSender
    {
        private readonly IEnumerable<ICommandHandler> _commandHandlers;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandSender" /> class.
        /// </summary>
        /// <param name="commandHandlers">
        ///     The handlers with which to handle commands.
        /// </param>
        public CommandSender(IEnumerable<ICommandHandler> commandHandlers)
        {
            this._commandHandlers = commandHandlers ?? throw new ArgumentNullException(nameof(commandHandlers));
        }

        /// <inheritdoc />
        public async Task<IResult> SendAsync(ICommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            Type handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());

            ICommandHandler commandHandler = this._commandHandlers.FirstOrDefault(handler => handler.GetType()
                .IsAssignableTo(handlerType));

            if (commandHandler == null)
            {
                return Result.Failure(new UnhandledCommandFailure());
            }

            return await commandHandler.HandleAsync(command);
        }
    }
}
