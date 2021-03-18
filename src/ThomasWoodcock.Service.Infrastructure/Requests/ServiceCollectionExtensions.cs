using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

using ThomasWoodcock.Service.Application.Common.Requests;

namespace ThomasWoodcock.Service.Infrastructure.Requests
{
    /// <summary>
    ///     Application request-related extension methods for <see cref="IServiceCollection" /> objects.
    /// </summary>
    internal static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Registers application request-related services with the given <paramref name="collection" />.
        /// </summary>
        /// <param name="collection">
        ///     The <see cref="IServiceCollection" /> with which to register the services.
        /// </param>
        public static void AddRequests(this IServiceCollection collection)
        {
            collection.AutoRegisterRequestHandlers();

            collection.AddSingleton<IRequestSender, MediatRRequestSender>();
        }

        private static void AutoRegisterRequestHandlers(this IServiceCollection collection)
        {
            // Gets all application request handlers.
            IEnumerable<Type> requestHandlers = typeof(IRequestHandler<,>).Assembly.GetTypes()
                .Where(type => !type.IsAbstract && type.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)));

            foreach (Type requestHandler in requestHandlers)
            {
                Type? requestHandlerInterface = requestHandler.GetInterface("IRequestHandler`2");

                if (requestHandlerInterface == null)
                {
                    continue;
                }

                Type requestType = requestHandlerInterface.GenericTypeArguments[0];
                Type responseType = requestHandlerInterface.GenericTypeArguments[1];

                Type request = typeof(MediatRRequest<,>).MakeGenericType(requestType, responseType);
                Type handlerInterface = typeof(MediatR.IRequestHandler<,>).MakeGenericType(request, responseType);

                Type handlerType =
                    typeof(MediatRRequestHandler<,,>).MakeGenericType(requestType, responseType, requestHandler);

                // Registers a respective MediatR request handler.
                collection.AddScoped(handlerInterface, handlerType);
            }
        }
    }
}
