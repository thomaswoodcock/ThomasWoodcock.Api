using System.Threading.Tasks;

using ThomasWoodcock.Service.Application.Accounts.Entities;
using ThomasWoodcock.Service.Domain.Accounts;

namespace ThomasWoodcock.Service.Application.Accounts.Commands
{
    /// <summary>
    ///     Allows a class to act as a repository for <see cref="AccountActivationKey" /> objects.
    /// </summary>
    public interface IAccountActivationKeyRepository
    {
        /// <summary>
        ///     Retrieves an activation key from the repository.
        /// </summary>
        /// <param name="account">
        ///     The account whose activation key will be retrieved.
        /// </param>
        /// <returns>
        ///     An activation key.
        /// </returns>
        Task<AccountActivationKey> GetAsync(Account account);

        /// <summary>
        ///     Adds an activation key to the repository.
        /// </summary>
        /// <param name="activationKey">
        ///     The activation key to add to the repository.
        /// </param>
        void Add(AccountActivationKey activationKey);

        /// <summary>
        ///     Saves any changes made to the repository.
        /// </summary>
        Task SaveAsync();
    }
}
