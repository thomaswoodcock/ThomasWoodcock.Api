using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

using ThomasWoodcock.Service.Application.Common.Commands.Validation;

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
            collection.AddCommandValidation();

            collection.AddScoped<ICommandSender, CommandSender>();
        }

        private static void AutoRegisterCommandHandlers(this IServiceCollection collection)
        {
            // Get all types that implement handler interface.
            IEnumerable<Type> handlerTypes = typeof(ServiceCollectionExtensions).Assembly.GetTypes()
                .Where(type => type.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<>)));

            foreach (Type handlerType in handlerTypes)
            {
                // Register handler.
                collection.AddScoped(typeof(ICommandHandler), handlerType);
            }
        }
    }
}
