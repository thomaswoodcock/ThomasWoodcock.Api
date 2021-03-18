using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

namespace ThomasWoodcock.Service.Application.Common.DomainEvents
{
    /// <summary>
    ///     Domain event-related extension methods for <see cref="IServiceCollection" /> objects.
    /// </summary>
    internal static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Registers domain event-related services with the given <paramref name="collection" />.
        /// </summary>
        /// <param name="collection">
        ///     The <see cref="IServiceCollection" /> with which to register the services.
        /// </param>
        public static void AddDomainEvents(this IServiceCollection collection)
        {
            collection.AutoRegisterDomainEventHandlers();
        }

        private static void AutoRegisterDomainEventHandlers(this IServiceCollection collection)
        {
            // Gets all domain event handlers.
            IEnumerable<Type> eventHandlers = typeof(IServiceCollection).Assembly.GetTypes()
                .Where(type => type.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDomainEventHandler<>)));

            foreach (Type eventHandler in eventHandlers)
            {
                // Registers the handler.
                collection.AddScoped(eventHandler);
            }
        }
    }
}
