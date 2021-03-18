using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

namespace ThomasWoodcock.Service.Application.Common.Commands
{
    /// <summary>
    ///     Command-related extension methods for <see cref="IServiceCollection" /> objects.
    /// </summary>
    internal static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Registers command-related services with the given <paramref name="collection" />.
        /// </summary>
        /// <param name="collection">
        ///     The <see cref="IServiceCollection" /> with which to register the services.
        /// </param>
        public static void AddCommands(this IServiceCollection collection)
        {
            collection.AutoRegisterCommandHandlers();

            collection.AddSingleton<ICommandSender, CommandSender>();
        }

        private static void AutoRegisterCommandHandlers(this IServiceCollection collection)
        {
            // Gets all command handlers.
            IEnumerable<Type> commandHandlers = typeof(ServiceCollectionExtensions).Assembly.GetTypes()
                .Where(type => type.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<>)));

            foreach (Type commandHandler in commandHandlers)
            {
                // Registers the handler.
                collection.AddScoped(commandHandler);
            }
        }
    }
}
