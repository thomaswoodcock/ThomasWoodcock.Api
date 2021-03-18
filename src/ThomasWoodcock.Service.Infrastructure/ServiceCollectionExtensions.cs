using MediatR;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ThomasWoodcock.Service.Infrastructure.Cryptography;
using ThomasWoodcock.Service.Infrastructure.DomainEvents;
using ThomasWoodcock.Service.Infrastructure.Notifications;
using ThomasWoodcock.Service.Infrastructure.Persistence;
using ThomasWoodcock.Service.Infrastructure.Requests;

namespace ThomasWoodcock.Service.Infrastructure
{
    /// <summary>
    ///     Infrastructure-related extension methods for <see cref="IServiceCollection" /> objects.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Registers infrastructure-related services with the given <paramref name="collection" />.
        /// </summary>
        /// <param name="collection">
        ///     The <see cref="IServiceCollection" /> with which to register the services.
        /// </param>
        /// <param name="configuration">
        ///     The <see cref="IConfiguration" /> used to access application configuration.
        /// </param>
        public static void AddInfrastructure(this IServiceCollection collection, IConfiguration configuration)
        {
            collection.AddCryptography();
            collection.AddDomainEvents();
            collection.AddNotifications();
            collection.AddPersistence(configuration);
            collection.AddRequests();

            collection.AddMediatR(typeof(ServiceCollectionExtensions));
        }
    }
}
