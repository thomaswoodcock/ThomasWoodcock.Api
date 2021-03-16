using System;

using ThomasWoodcock.Service.Application.Accounts.Entities;
using ThomasWoodcock.Service.Domain.Accounts;
using ThomasWoodcock.Service.Infrastructure.Persistence.Accounts;

using Xunit;

namespace ThomasWoodcock.Service.Infrastructure.UnitTests.Persistence.Accounts
{
    public sealed class EfAccountDatabaseFixture : EfInMemoryDatabaseFixture<AccountContext>
    {
        public EfAccountDatabaseFixture()
        {
            using AccountContext context = new(this.ContextOptions);

            context.Database.EnsureCreated();

            // Accounts
            this.Account = Account.Create(new Guid("191D440E-DEB4-4066-8529-371883A43144"), "Test Name",
                    "test@test.com", "TestPassword123")
                .Value ?? throw new InvalidOperationException();

            this.SecondAccount = Account.Create(new Guid("9C55DC7D-8019-4EDB-B866-A23FD56089E2"), "Test Name",
                    "second@test.com", "TestPassword123")
                .Value ?? throw new InvalidOperationException();

            Account accountForDeletedKey = Account.Create(new Guid("5A2A4A61-182C-4015-82C5-49AECC7C6886"), "Test Name",
                    "another@test.com", "TestPassword123")
                .Value ?? throw new InvalidOperationException();

            context.Accounts.AddRange(this.Account, this.SecondAccount, accountForDeletedKey);

            // Activation Keys
            this.ActivationKey = new AccountActivationKey(new Guid("B1597BB8-4119-48D0-BCBD-E33C82FD0031"));

            context.Entry(this.ActivationKey)
                .Property<Guid>("AccountId")
                .CurrentValue = this.Account.Id;

            this.ActivationKeyToBeDeleted = new AccountActivationKey(new Guid("E1EA7B36-1F89-4D83-BE7B-6464CCCE02B9"));

            context.Entry(this.ActivationKeyToBeDeleted)
                .Property<Guid>("AccountId")
                .CurrentValue = accountForDeletedKey.Id;

            context.ActivationKeys.AddRange(this.ActivationKey, this.ActivationKeyToBeDeleted);

            context.SaveChanges();
        }

        internal Account Account { get; }
        internal Account SecondAccount { get; }
        internal AccountActivationKey ActivationKey { get; }
        internal AccountActivationKey ActivationKeyToBeDeleted { get; }
    }

    [CollectionDefinition("EfAccountDatabaseCollection")]
    public sealed class EfAccountDatabaseCollection : ICollectionFixture<EfAccountDatabaseFixture>
    {
    }
}
