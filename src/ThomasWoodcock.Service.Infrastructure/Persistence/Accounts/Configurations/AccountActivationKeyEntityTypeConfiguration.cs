using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ThomasWoodcock.Service.Application.Accounts.Entities;
using ThomasWoodcock.Service.Domain.Accounts;

namespace ThomasWoodcock.Service.Infrastructure.Persistence.Accounts.Configurations
{
    /// <inheritdoc />
    /// <summary>
    ///     An implementation of the <see cref="IEntityTypeConfiguration{TEntity}" /> interface used to configure Entity
    ///     Framework <see cref="AccountActivationKey" /> entities.
    /// </summary>
    public sealed class AccountActivationKeyEntityTypeConfiguration : IEntityTypeConfiguration<AccountActivationKey>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<AccountActivationKey> builder)
        {
            builder.Property(key => key.Value)
                .IsRequired()
                .ValueGeneratedNever();

            builder.HasKey(key => key.Value);

            builder.Property<Guid>("AccountId")
                .IsRequired()
                .ValueGeneratedNever();

            builder.HasOne<Account>()
                .WithOne()
                .HasForeignKey<AccountActivationKey>("AccountId");
        }
    }
}
