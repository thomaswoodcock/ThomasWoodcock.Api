using Microsoft.Extensions.DependencyInjection;

using ThomasWoodcock.Service.Application.Common.Cryptography;

namespace ThomasWoodcock.Service.Infrastructure.Cryptography
{
    /// <summary>
    ///     Cryptography-related extension methods for <see cref="IServiceCollection" /> objects.
    /// </summary>
    internal static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Registers cryptography-related services with the given <paramref name="collection" />.
        /// </summary>
        /// <param name="collection">
        ///     The <see cref="IServiceCollection" /> with which to register the services.
        /// </param>
        public static void AddCryptography(this IServiceCollection collection)
        {
            collection.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();
        }
    }
}
