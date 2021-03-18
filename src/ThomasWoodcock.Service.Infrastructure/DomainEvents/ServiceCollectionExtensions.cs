using System;
using System.Collections.Generic;
using System.Linq;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using ThomasWoodcock.Service.Application.Common.DomainEvents;

namespace ThomasWoodcock.Service.Infrastructure.DomainEvents
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
            collection.AutoRegisterDomainEventNotificationHandlers();

            collection.AddSingleton<IDomainEventPublisher, MediatRDomainEventPublisher>();
        }

        private static void AutoRegisterDomainEventNotificationHandlers(this IServiceCollection collection)
        {
            // Gets all domain event handlers.
            IEnumerable<Type> eventHandlers = typeof(IDomainEventHandler<>).Assembly.GetTypes()
                .Where(type => type.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDomainEventHandler<>)));

            foreach (Type eventHandler in eventHandlers)
            {
                Type? eventType = eventHandler.GetInterface("IDomainEventHandler`1")
                    ?.GenericTypeArguments[0];

                if (eventType == null)
                {
                    continue;
                }

                Type notificationType = typeof(MediatRDomainEventNotification<>).MakeGenericType(eventType);
                Type handlerInterface = typeof(INotificationHandler<>).MakeGenericType(notificationType);

                Type handlerType =
                    typeof(MediatRDomainEventNotificationHandler<,>).MakeGenericType(eventType, eventHandler);

                // Registers a respective MediatR notification handler.
                collection.AddScoped(handlerInterface, handlerType);
            }
        }
    }
}
