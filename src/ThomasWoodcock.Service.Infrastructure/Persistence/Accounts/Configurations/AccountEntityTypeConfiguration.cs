using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ThomasWoodcock.Service.Domain.Accounts;

namespace ThomasWoodcock.Service.Infrastructure.Persistence.Accounts.Configurations
{
    /// <inheritdoc />
    /// <summary>
    ///     An implementation of the <see cref="IEntityTypeConfiguration{TEntity}" /> interface used to configure Entity
    ///     Framework <see cref="Account" /> entities.
    /// </summary>
    public sealed class AccountEntityTypeConfiguration : IEntityTypeConfiguration<Account>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.Property(account => account.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.HasKey(account => account.Id);

            builder.Property(account => account.Name)
                .IsRequired();

            builder.OwnsOne(account => account.EmailAddress, emailAddress => emailAddress.Property<string>("_value")
                .IsRequired());

            builder.Navigation(account => account.EmailAddress)
                .IsRequired();

            builder.Property(account => account.Password)
                .IsRequired();

            builder.Property(account => account.IsActive)
                .IsRequired();
        }
    }
}
