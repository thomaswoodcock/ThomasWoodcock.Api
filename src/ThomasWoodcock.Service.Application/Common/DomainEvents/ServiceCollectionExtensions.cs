using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

using ThomasWoodcock.Service.Application.Common.Notifications;

namespace ThomasWoodcock.Service.Application.Common.DomainEvents
{
    internal static class ServiceCollectionExtensions
    {
        public static void AddDomainEvents(this IServiceCollection collection)
        {
            // TODO: Remove when notifications implemented.
            collection.AddSingleton<INotificationSender, NotificationSender>();

            collection.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
            collection.AutoRegisterEventHandlers();
        }

        private static void AutoRegisterEventHandlers(this IServiceCollection collection)
        {
            // Get all types that implement handler interface.
            IEnumerable<Type> handlerTypes = typeof(ServiceCollectionExtensions).Assembly.GetTypes()
                .Where(type => type.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDomainEventHandler<>)));

            foreach (Type handlerType in handlerTypes)
            {
                Type interfaceType = handlerType.GetInterface("IDomainEventHandler`1");

                if (interfaceType == null)
                {
                    continue;
                }

                // Register handler.
                collection.AddScoped(typeof(IDomainEventHandler), handlerType);
                collection.AddScoped(interfaceType, handlerType);
            }
        }
    }
}
