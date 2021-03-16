using System;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using ThomasWoodcock.Service.Application.Accounts.Commands;
using ThomasWoodcock.Service.Application.Accounts.Entities;
using ThomasWoodcock.Service.Domain.Accounts;

namespace ThomasWoodcock.Service.Infrastructure.Persistence.Accounts
{
    /// <inheritdoc />
    /// <summary>
    ///     An implementation of the <see cref="IAccountActivationKeyRepository" /> interface used to interact with Entity
    ///     Framework context.
    /// </summary>
    internal sealed class EfAccountActivationKeyRepository : IAccountActivationKeyRepository
    {
        private readonly AccountContext _context;

        /// <summary>
        ///     Initializes a new instance of the <see cref="EfAccountActivationKeyRepository" /> class.
        /// </summary>
        /// <param name="context">
        ///     The <see cref="AccountContext" /> with which to interact.
        /// </param>
        public EfAccountActivationKeyRepository(AccountContext context)
        {
            this._context = context;
        }

        /// <inheritdoc />
        public async Task<AccountActivationKey?> GetAsync(Account account)
        {
            return await this._context.ActivationKeys.SingleOrDefaultAsync(key =>
                EF.Property<Guid>(key, "AccountId") == account.Id);
        }

        /// <inheritdoc />
        public void Add(Account account, AccountActivationKey activationKey)
        {
            this._context.Add(activationKey);

            this._context.Entry(activationKey)
                .Property<Guid>("AccountId")
                .CurrentValue = account.Id;
        }

        /// <inheritdoc />
        public void Remove(AccountActivationKey activationKey)
        {
            this._context.Remove(activationKey);
        }

        /// <inheritdoc />
        public async Task SaveAsync() => await this._context.SaveChangesAsync();
    }
}
