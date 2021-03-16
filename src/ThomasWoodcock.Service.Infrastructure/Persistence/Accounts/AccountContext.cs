#pragma warning disable 8618

using Microsoft.EntityFrameworkCore;

using ThomasWoodcock.Service.Application.Accounts.Entities;
using ThomasWoodcock.Service.Domain.Accounts;
using ThomasWoodcock.Service.Infrastructure.Persistence.Accounts.Configurations;

namespace ThomasWoodcock.Service.Infrastructure.Persistence.Accounts
{
    /// <inheritdoc />
    /// <summary>
    ///     An implementation of the <see cref="DbContext" /> class used to provide Entity Framework context for accounts.
    /// </summary>
    public sealed class AccountContext : DbContext
    {
        /// <inheritdoc />
        public AccountContext(DbContextOptions<AccountContext> options)
            : base(options)
        {
        }

        /// <summary>
        ///     Gets or sets the <see cref="Account" /> objects within the context.
        /// </summary>
        internal DbSet<Account> Accounts { get; init; }

        /// <summary>
        ///     Gets or sets the <see cref="AccountActivationKey" /> objects within the context.
        /// </summary>
        internal DbSet<AccountActivationKey> ActivationKeys { get; init; }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new AccountEntityTypeConfiguration().Configure(modelBuilder.Entity<Account>());
            new AccountActivationKeyEntityTypeConfiguration().Configure(modelBuilder.Entity<AccountActivationKey>());

            base.OnModelCreating(modelBuilder);
        }
    }
}
