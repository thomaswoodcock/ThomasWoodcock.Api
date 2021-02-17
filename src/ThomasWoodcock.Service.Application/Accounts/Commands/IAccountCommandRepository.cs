using System;
using System.Threading.Tasks;

using ThomasWoodcock.Service.Domain.Accounts;

namespace ThomasWoodcock.Service.Application.Accounts.Commands
{
    /// <summary>
    ///     Allows a class to act as a repository for <see cref="Account" /> objects.
    /// </summary>
    public interface IAccountCommandRepository
    {
        /// <summary>
        ///     Retrieves an account with the given <paramref name="id" /> from the repository.
        /// </summary>
        /// <param name="id">
        ///     The ID of the account to retrieve from the repository.
        /// </param>
        /// <returns>
        ///     The account associated with the given <paramref name="id" />.
        /// </returns>
        Task<Account> GetAsync(Guid id);

        /// <summary>
        ///     Retrieves an account with the given <paramref name="emailAddress" /> from the repository.
        /// </summary>
        /// <param name="emailAddress">
        ///     The email address of the account to retrieve from the repository.
        /// </param>
        /// <returns>
        ///     The account associated with the given <paramref name="emailAddress" />.
        /// </returns>
        Task<Account> GetAsync(string emailAddress);

        /// <summary>
        ///     Adds an account to the repository.
        /// </summary>
        /// <param name="account">
        ///     The account to add to the repository.
        /// </param>
        void Add(Account account);

        /// <summary>
        ///     Saves any changes made to the repository.
        /// </summary>
        Task SaveAsync();
    }
}
