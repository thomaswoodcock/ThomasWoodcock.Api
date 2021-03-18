using System;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using ThomasWoodcock.Service.Application.Accounts.Commands;
using ThomasWoodcock.Service.Domain.Accounts;

namespace ThomasWoodcock.Service.Infrastructure.Persistence.Accounts
{
    /// <inheritdoc />
    /// <summary>
    ///     An implementation of the <see cref="IAccountCommandRepository" /> interface used to interact with Entity Framework
    ///     context.
    /// </summary>
    internal sealed class EfAccountCommandRepository : IAccountCommandRepository
    {
        private readonly AccountContext _context;

        /// <summary>
        ///     Initializes a new instance of the <see cref="EfAccountCommandRepository" /> class.
        /// </summary>
        /// <param name="context">
        ///     The <see cref="AccountContext" /> with which to interact.
        /// </param>
        public EfAccountCommandRepository(AccountContext context)
        {
            this._context = context;
        }

        /// <inheritdoc />
        public async Task<Account?> GetAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return await this._context.Accounts.SingleOrDefaultAsync(account => account.Id == id);
        }

        /// <inheritdoc />
        public async Task<Account?> GetAsync(string emailAddress)
        {
            if (string.IsNullOrWhiteSpace(emailAddress))
            {
                throw new ArgumentNullException(nameof(emailAddress));
            }

            return await this._context.Accounts.SingleOrDefaultAsync(account =>
                EF.Property<string>(account.EmailAddress, "_value") == emailAddress);
        }

        /// <inheritdoc />
        public void Add(Account account)
        {
            this._context.Accounts.Add(account);
        }

        /// <inheritdoc />
        public async Task SaveAsync() => await this._context.SaveChangesAsync();
    }
}
