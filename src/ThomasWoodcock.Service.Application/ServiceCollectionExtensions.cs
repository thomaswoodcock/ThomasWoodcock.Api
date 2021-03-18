using Microsoft.Extensions.DependencyInjection;

using ThomasWoodcock.Service.Application.Common.DomainEvents;

namespace ThomasWoodcock.Service.Application
{
    /// <summary>
    ///     Application-related extension methods for <see cref="IServiceCollection" /> objects.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Registers application-related services with the given <paramref name="collection" />.
        /// </summary>
        /// <param name="collection">
        ///     The <see cref="IServiceCollection" /> with which to register the services.
        /// </param>
        public static void AddApplication(this IServiceCollection collection)
        {
            collection.AddDomainEvents();
        }
    }
}
