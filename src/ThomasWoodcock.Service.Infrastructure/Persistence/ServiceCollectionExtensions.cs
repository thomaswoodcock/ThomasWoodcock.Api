using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ThomasWoodcock.Service.Application.Accounts.Commands;
using ThomasWoodcock.Service.Infrastructure.Persistence.Accounts;

namespace ThomasWoodcock.Service.Infrastructure.Persistence
{
    /// <summary>
    ///     Persistence-related extension methods for <see cref="IServiceCollection" /> objects.
    /// </summary>
    internal static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Registers persistence-related services with the given <paramref name="collection" />.
        /// </summary>
        /// <param name="collection">
        ///     The <see cref="IServiceCollection" /> with which to register the services.
        /// </param>
        /// <param name="configuration">
        ///     The <see cref="IConfiguration" /> used to access application configuration.
        /// </param>
        internal static void AddPersistence(this IServiceCollection collection, IConfiguration configuration)
        {
            var options = configuration.GetSection(nameof(PersistenceOptions))
                .Get<PersistenceOptions>();

            string databaseConnection = configuration.GetConnectionString("Database");

            collection.AddDbContext<AccountContext>(builder =>
            {
                if (options.UseSqliteDatabase)
                {
                    builder.UseSqlite(databaseConnection,
                        opt => opt.MigrationsAssembly(typeof(AccountContext).Assembly.FullName));
                }
                else
                {
                    builder.UseCosmos(databaseConnection, options.DatabaseName);
                }
            });

            collection.AddScoped<IAccountCommandRepository, EfAccountCommandRepository>();
            collection.AddScoped<IAccountActivationKeyRepository, EfAccountActivationKeyRepository>();
        }
    }
}
