using System;
using System.Threading.Tasks;

using ThomasWoodcock.Service.Domain.Accounts;

namespace ThomasWoodcock.Service.Application.Accounts.Services
{
    /// <summary>
    ///     Allows a class to act as a service for generating account activation keys.
    /// </summary>
    public interface IAccountActivationKeyGenerator
    {
        /// <summary>
        ///     Generate an activation key for an account.
        /// </summary>
        /// <param name="account">
        ///     The account for which the activation key will be generated.
        /// </param>
        /// <returns>
        ///     The activation key for the account.
        /// </returns>
        Task<Guid> GenerateAsync(Account account);
    }
}
